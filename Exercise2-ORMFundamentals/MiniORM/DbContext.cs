namespace MiniORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;

    public abstract class DbContext
    {
	private readonly DbConnection connection;
	private readonly Dictionary<Type, PropertyInfo> dbSetProperties;

	protected DbContext(string connectionString)
	{
	    connection = new DbConnection(connectionString);
	    dbSetProperties = DiscoverDbSetProperties();
	    using (new ConnectionManager(connection))
	    {
		InitializeDbSets();
	    }
	    MapAllRelations();
	}

	private Dictionary<Type, PropertyInfo> DiscoverDbSetProperties()
	{
	    Dictionary<Type, PropertyInfo> dbSetProperties = GetType().GetProperties()
		.Where(pi => pi.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
		.ToDictionary(pi => pi.PropertyType.GetGenericArguments().Single(), pi => pi);
	    return dbSetProperties;
	}

	private IEnumerable<string> GetEntityColumnNames(Type table)
	{
	    string tableName = GetTableName(table);
	    IEnumerable<string> dbColumns = connection.FetchColumnNames(tableName);
	    string[] columnNames = table.GetProperties()
		.Where(pi => dbColumns.Contains(pi.Name)
		    && !pi.HasAttribute<NotMappedAttribute>()
		    && DbConfiguration.AllowedSqlTypes.Contains(pi.PropertyType))
		.Select(pi => pi.Name).ToArray();
	    return columnNames;
	}

	private string GetTableName(Type table)
	{
	    string tableName = ((TableAttribute)Attribute
		.GetCustomAttribute(table, typeof(TableAttribute)))?.Name;
	    if (String.IsNullOrEmpty(tableName))
		tableName = this.dbSetProperties[table].Name;
	    return tableName;
	}

	private void InitializeDbSets()
	{
	    foreach (var dbSet in dbSetProperties)
	    {
		Type dbSetType = dbSet.Key;
		PropertyInfo dbSetProperty = dbSet.Value;
		MethodInfo populateDbSetMethod = typeof(DbContext)
		    .GetMethod("PopulateDbSet", BindingFlags.Instance | BindingFlags.NonPublic)
		    .MakeGenericMethod(dbSetType);
		populateDbSetMethod.Invoke(this, new object[] { dbSetProperty });
	    }
	}

	private static bool IsObjectValid(object entity)
	{
	    ValidationContext context = new ValidationContext(entity);
	    ICollection<ValidationResult> results = new List<ValidationResult>();
	    bool isEntityValid = Validator.TryValidateObject(entity, context, results, true);
	    return isEntityValid;
	}

	private IEnumerable<TEntity> LoadTableEntities<TEntity>() where TEntity : class, new()
	{
	    Type table = typeof(TEntity);
	    string tableName = GetTableName(table);
	    string[] columns = GetEntityColumnNames(table).ToArray();
	    IEnumerable<TEntity> resultSet = connection.FetchResultSet<TEntity>(tableName, columns);
	    return resultSet;
	}

	private void MapAllRelations()
	{
	    foreach (var dbSetProperty in dbSetProperties)
	    {
		Type dbSetType = dbSetProperty.Key;
		MethodInfo mapRelationsMethod = typeof(DbContext)
		    .GetMethod("MapRelations", BindingFlags.Instance | BindingFlags.NonPublic)
		    .MakeGenericMethod(dbSetType);
		object dbSet = dbSetProperty.Value.GetValue(this);
		mapRelationsMethod.Invoke(this, new object[] { dbSet });
	    }
	}

	private void MapCollection<TEntity, TCollection>(DbSet<TEntity> dbSet, PropertyInfo collectionProperty)
	    where TEntity : class, new() where TCollection : class, new()
	{
	    Type entityType = typeof(TEntity);
	    Type collectionType = typeof(TCollection);
	    PropertyInfo[] primaryKeys = collectionType.GetProperties()
		.Where(pi => pi.HasAttribute<KeyAttribute>()).ToArray();
	    PropertyInfo primaryKey = primaryKeys.First();
	    PropertyInfo foreignKey = entityType.GetProperties()
		.First(pi => pi.HasAttribute<KeyAttribute>());
	    bool isManyToManyRelation = primaryKeys.Length >= 2;
	    if (isManyToManyRelation)
	    {
		primaryKey = collectionType.GetProperties()
		    .First(pi => collectionType.GetProperty(pi.GetCustomAttribute<ForeignKeyAttribute>().Name).PropertyType == entityType);
	    }
	    var navigationDbSet = (DbSet<TCollection>)dbSetProperties[collectionType].GetValue(this);
	    foreach (TEntity entity in dbSet)
	    {
		object primaryKeyValue = foreignKey.GetValue(entity);
		var navigationEntities = navigationDbSet
		    .Where(navEntity => primaryKey.GetValue(navEntity).Equals(primaryKeyValue)).ToArray();
		ReflectionHelper.ReplaceBackingField(entity, collectionProperty.Name, navigationEntities);
	    }
	}

	private void MapNavigationProperties<TEntity>(DbSet<TEntity> dbSet) where TEntity : class, new()
	{
	    Type entityType = typeof(TEntity);
	    PropertyInfo[] foreignKeys = entityType.GetProperties()
		.Where(pi => pi.HasAttribute<ForeignKeyAttribute>()).ToArray();
	    foreach (PropertyInfo foreignKey in foreignKeys)
	    {
		string navigationPropertyName = foreignKey.GetCustomAttribute<ForeignKeyAttribute>().Name;
		PropertyInfo navigationProperty = entityType.GetProperty(navigationPropertyName);
		object navigationDbSet = dbSetProperties[navigationProperty.PropertyType].GetValue(this);
		var navigationPropertyPrimaryKey = navigationProperty.PropertyType
		    .GetProperties().First(pi => pi.HasAttribute<KeyAttribute>());
		foreach (TEntity entity in dbSet)
		{
		    object foreignKeyValue = foreignKey.GetValue(entity);
		    object navigationPropertyValue = ((IEnumerable<object>)navigationDbSet)
			.First(currentNavigationProperty => navigationPropertyPrimaryKey
			    .GetValue(currentNavigationProperty).Equals(foreignKeyValue));
		    navigationProperty.SetValue(entity, navigationPropertyValue);
		}
	    }
	}

	private void MapRelations<TEntity>(DbSet<TEntity> dbSet) where TEntity : class, new()
	{
	    Type entityType = typeof(TEntity);
	    MapNavigationProperties(dbSet);
	    PropertyInfo[] collections = entityType.GetProperties()
		.Where(pi => pi.PropertyType.IsGenericType
		&& pi.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>)).ToArray();
	    foreach (PropertyInfo collection in collections)
	    {
		Type collectionType = collection.PropertyType.GetGenericArguments().Single();
		MethodInfo mapCollectionMethod = typeof(DbContext)
		    .GetMethod("MapCollection", BindingFlags.Instance | BindingFlags.NonPublic)
		    .MakeGenericMethod(entityType, collectionType);
		mapCollectionMethod.Invoke(this, new object[] { dbSet, collection });
	    }
	}

	private void Persist<TEntity>(DbSet<TEntity> dbSet) where TEntity : class, new()
	{
	    string tableName = GetTableName(typeof(TEntity));
	    string[] columns = connection.FetchColumnNames(tableName).ToArray();
	    if (dbSet.ChangeTracker.Added.Any())
		connection.InsertEntities(dbSet.ChangeTracker.Added, tableName, columns);
	    if (dbSet.ChangeTracker.Modified.Any())
		connection.UpdateEntities(dbSet.ChangeTracker.Modified, tableName, columns);
	    if (dbSet.ChangeTracker.Removed.Any())
		connection.DeleteEntities(dbSet.ChangeTracker.Removed, tableName, columns);
	    dbSet.ChangeTracker.Reset();
	}

	private void PopulateDbSet<TEntity>(PropertyInfo dbSetProperty) where TEntity : class, new()
	{
	    IEnumerable<TEntity> entities = LoadTableEntities<TEntity>();
	    DbSet<TEntity> dbSet = new DbSet<TEntity>(entities);
	    ReflectionHelper.ReplaceBackingField(this, dbSetProperty.Name, dbSet);
	}

	public void SaveChanges()
	{
	    object[] dbSets = dbSetProperties.Select(pi => pi.Value.GetValue(this)).ToArray();
	    using (new ConnectionManager(connection))
	    {
		using (SqlTransaction transaction = connection.StartTransaction())
		{
		    foreach (IEnumerable<object> dbSet in dbSets)
		    {
			var invalidEntities = dbSet.Where(entity => !IsObjectValid(entity)).ToArray();
			if (invalidEntities.Any())
			    throw new InvalidOperationException($"{invalidEntities.Length} invalid entities found in {dbSet.GetType().Name}!");
			Type dbSetType = dbSet.GetType().GetGenericArguments().Single();
			MethodInfo persistMethod = typeof(DbContext)
			    .GetMethod("Persist", BindingFlags.Instance | BindingFlags.NonPublic)
			    .MakeGenericMethod(dbSetType);
			try
			{
			    persistMethod.Invoke(this, new object[] { dbSet });
			}
			catch (Exception exception)
			{
			    if (exception is TargetInvocationException tie)
				throw tie.InnerException;
			    else if (exception is SqlException || exception is InvalidOperationException)
			    {
				transaction.Rollback();
				throw;
			    }
			}
		    }
		    transaction.Commit();
		    InitializeDbSets();
		}
	    }
	}
    }
}
