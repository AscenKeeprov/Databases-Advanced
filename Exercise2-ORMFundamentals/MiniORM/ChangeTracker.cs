namespace MiniORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    internal class ChangeTracker<TEntity> where TEntity : class, new()
    {
	private readonly List<TEntity> allEntities;
	private readonly List<TEntity> added;
	private readonly List<TEntity> modified;
	private readonly List<TEntity> removed;

	public ChangeTracker(IEnumerable<TEntity> entities)
	{
	    allEntities = CloneEntities(entities).ToList();
	    added = new List<TEntity>();
	    modified = new List<TEntity>();
	    removed = new List<TEntity>();
	}

	public IReadOnlyCollection<TEntity> AllEntities => allEntities.AsReadOnly();
	public IReadOnlyCollection<TEntity> Added => added.AsReadOnly();
	public IReadOnlyCollection<TEntity> Modified => modified.AsReadOnly();
	public IReadOnlyCollection<TEntity> Removed => removed.AsReadOnly();

	public void Add(TEntity entity) => added.Add(entity);

	private static IEnumerable<TEntity> CloneEntities(IEnumerable<TEntity> entities)
	{
	    List<TEntity> clonedEntities = new List<TEntity>();
	    PropertyInfo[] propertiesToClone = typeof(TEntity).GetProperties()
		.Where(pi => DbConfiguration.AllowedSqlTypes.Contains(pi.PropertyType)).ToArray();
	    foreach (var entity in entities)
	    {
		TEntity clonedEntity = Activator.CreateInstance<TEntity>();
		foreach (var property in propertiesToClone)
		{
		    var originalValue = property.GetValue(entity);
		    property.SetValue(clonedEntity, originalValue);
		}
		clonedEntities.Add(clonedEntity);
	    }
	    return clonedEntities;
	}

	public IEnumerable<TEntity> GetModifiedEntities(DbSet<TEntity> dbSet)
	{
	    List<TEntity> modifiedEntities = new List<TEntity>();
	    PropertyInfo[] primaryKeys = typeof(TEntity).GetProperties()
		.Where(pi => pi.HasAttribute<KeyAttribute>()).ToArray();
	    foreach (TEntity originalEntity in AllEntities)
	    {
		object[] primaryKeyValues = GetPrimaryKeyValues(primaryKeys, originalEntity).ToArray();
		TEntity dbSetEntity = dbSet.Entities.Single(e
		    => GetPrimaryKeyValues(primaryKeys, e).SequenceEqual(primaryKeyValues));
		bool isEntityModified = IsModified(originalEntity, dbSetEntity);
		if (isEntityModified) modifiedEntities.Add(dbSetEntity);
	    }
	    return modifiedEntities;
	}

	private static IEnumerable<object> GetPrimaryKeyValues(IEnumerable<PropertyInfo> primaryKeys, TEntity entity)
	{
	    object[] primaryKeyValues = primaryKeys
		.Select(pi => pi.GetValue(entity)).ToArray();
	    return primaryKeyValues;
	}

	private static bool IsModified(TEntity originalEntity, TEntity dbSetEntity)
	{
	    PropertyInfo[] monitoredProperties = typeof(TEntity).GetProperties()
		.Where(pi => DbConfiguration.AllowedSqlTypes.Contains(pi.PropertyType)).ToArray();
	    PropertyInfo[] modifiedProperties = monitoredProperties
		.Where(pi => !Equals(pi.GetValue(originalEntity), pi.GetValue(dbSetEntity))).ToArray();
	    bool isEntityModified = modifiedProperties.Any();
	    return isEntityModified;
	}

	public void Remove(TEntity entity) => removed.Add(entity);

	public void Reset()
	{
	    added.Clear();
	    modified.Clear();
	    removed.Clear();
	}

	public void Update(TEntity entity) => modified.Add(entity);
    }
}
