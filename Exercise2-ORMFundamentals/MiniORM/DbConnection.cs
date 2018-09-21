namespace MiniORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Used for accessing a database, inserting/updating/deleting entities
    /// and mapping database columns to entity classes.
    /// </summary>
    internal class DbConnection
    {
	private readonly SqlConnection connection;
	private SqlTransaction transaction;

	public DbConnection(string connectionString)
	{
	    connection = new SqlConnection(connectionString);
	}

	public void Close() => connection.Close();

	private SqlCommand CreateCommand(string queryText, params SqlParameter[] parameters)
	{
	    SqlCommand command = new SqlCommand(queryText, connection, transaction);
	    foreach (SqlParameter parameter in parameters)
	    {
		command.Parameters.Add(parameter);
	    }
	    return command;
	}

	public void DeleteEntities<TEntity>(IEnumerable<TEntity> entitiesToDelete, string tableName, string[] columns)
	    where TEntity : class
	{
	    PropertyInfo[] primaryKeyProperties = typeof(TEntity).GetProperties()
		.Where(pi => pi.HasAttribute<KeyAttribute>()).ToArray();
	    foreach (TEntity entity in entitiesToDelete)
	    {
		object[] primaryKeyValues = primaryKeyProperties
		    .Select(pi => pi.GetValue(entity)).ToArray();
		SqlParameter[] primaryKeyParameters = primaryKeyProperties
		    .Zip(primaryKeyValues, (param, value) => new SqlParameter(param.Name, value)).ToArray();
		string primaryKeysSql = String.Join(" AND ",
		    primaryKeyProperties.Select(pk => $"{pk.Name} = @{pk.Name}"));
		string queryText = String.Format("DELETE FROM {0} WHERE {1}", tableName, primaryKeysSql);
		int affectedRows = ExecuteNonQuery(queryText, primaryKeyParameters);
		if (affectedRows != 1)
		    throw new InvalidOperationException($"Delete for table {tableName} failed.");
	    }
	}

	private static string EscapeColumn(string columnName)
	{
	    string escapedColumn = $"[{columnName}]";
	    return escapedColumn;
	}

	public IEnumerable<T> ExecuteQuery<T>(string queryText)
	{
	    List<T> fields = new List<T>();
	    using (SqlCommand command = CreateCommand(queryText))
	    {
		using (SqlDataReader reader = command.ExecuteReader())
		{
		    while (reader.Read())
		    {
			int columnsCount = reader.FieldCount;
			object[] columnValues = new object[columnsCount];
			reader.GetValues(columnValues);
			T field = reader.GetFieldValue<T>(0);
			fields.Add(field);
		    }
		}
	    }
	    return fields;
	}

	public int ExecuteNonQuery(string queryText, params SqlParameter[] parameters)
	{
	    using (SqlCommand command = CreateCommand(queryText, parameters))
	    {
		int rowsAffected = command.ExecuteNonQuery();
		return rowsAffected;
	    }
	}

	public IEnumerable<string> FetchColumnNames(string tableName)
	{
	    List<string> columnNames = new List<string>();
	    string queryText = $@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
	    using (SqlCommand command = CreateCommand(queryText))
	    {
		using (SqlDataReader reader = command.ExecuteReader())
		{
		    while (reader.Read())
		    {
			string columnName = reader.GetString(0);
			columnNames.Add(columnName);
		    }
		}
	    }
	    return columnNames;
	}

	public IEnumerable<T> FetchResultSet<T>(string tableName, params string[] columnNames)
	{
	    List<T> rows = new List<T>();
	    string escapedColumns = String.Join(", ", columnNames.Select(EscapeColumn));
	    string queryText = $@"SELECT {escapedColumns} FROM {tableName}";
	    using (SqlCommand query = CreateCommand(queryText))
	    {
		using (SqlDataReader reader = query.ExecuteReader())
		{
		    while (reader.Read())
		    {
			int columnsCount = reader.FieldCount;
			object[] columnValues = new object[columnsCount];
			reader.GetValues(columnValues);
			T row = MapColumnsToObject<T>(columnNames, columnValues);
			rows.Add(row);
		    }
		}
	    }
	    return rows;
	}

	private IEnumerable<string> GetIdentityColumns(string tableName)
	{
	    const string identityColumnsSql =
		    "SELECT COLUMN_NAME FROM (SELECT COLUMN_NAME, COLUMNPROPERTY(OBJECT_ID(TABLE_NAME), COLUMN_NAME, 'IsIdentity') AS IsIdentity FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}') AS IdentitySpecs WHERE IsIdentity = 1";
	    string parametrizedSql = String.Format(identityColumnsSql, tableName);
	    IEnumerable<string> identityColumns = ExecuteQuery<string>(parametrizedSql);
	    return identityColumns;
	}

	public void InsertEntities<TEntity>(IEnumerable<TEntity> entitiesToInsert, string tableName, string[] columns)
	    where TEntity : class
	{
	    IEnumerable<string> identityColumns = GetIdentityColumns(tableName);
	    string[] columnsToInsert = columns.Except(identityColumns).ToArray();
	    string[] escapedColumns = columnsToInsert.Select(EscapeColumn).ToArray();
	    object[][] rowValues = entitiesToInsert.Select(entity => columnsToInsert.Select(c
		=> entity.GetType().GetProperty(c).GetValue(entity)).ToArray()).ToArray();
	    string[][] rowParameterNames = Enumerable.Range(1, rowValues.Length)
		.Select(i => columnsToInsert.Select(c => c + i).ToArray()).ToArray();
	    string columnsSql = String.Join(", ", escapedColumns);
	    string rowsSql = String.Join(", ", rowParameterNames.Select(p
		=> String.Format("({0})", String.Join(", ", p.Select(a => $"@{a}")))));
	    string queryText = String.Format("INSERT INTO {0} ({1}) VALUES {2}", tableName, columnsSql, rowsSql);
	    SqlParameter[] parameters = rowParameterNames
		.Zip(rowValues, (@params, values) => @params.Zip(values, (param, value)
		=> new SqlParameter(param, value ?? DBNull.Value))).SelectMany(p => p).ToArray();
	    int insertedRows = ExecuteNonQuery(queryText, parameters);
	    if (insertedRows != entitiesToInsert.Count())
		throw new InvalidOperationException($"Could not insert {entitiesToInsert.Count() - insertedRows} rows.");
	}

	private static T MapColumnsToObject<T>(string[] columnNames, object[] columns)
	{
	    T obj = Activator.CreateInstance<T>();
	    for (int i = 0; i < columns.Length; i++)
	    {
		string columnName = columnNames[i];
		object columnValue = columns[i];
		if (columnValue is DBNull) columnValue = null;
		PropertyInfo property = typeof(T).GetProperty(columnName);
		property.SetValue(obj, columnValue);
	    }
	    return obj;
	}

	public void Open() => connection.Open();

	public SqlTransaction StartTransaction()
	{
	    transaction = connection.BeginTransaction();
	    return transaction;
	}

	public void UpdateEntities<TEntity>(IEnumerable<TEntity> entitiesToUpdate, string tableName, string[] columns)
	    where TEntity : class
	{
	    IEnumerable<string> identityColumns = GetIdentityColumns(tableName);
	    string[] columnsToUpdate = columns.Except(identityColumns).ToArray();
	    PropertyInfo[] primaryKeyProperties = typeof(TEntity).GetProperties()
		.Where(pi => pi.HasAttribute<KeyAttribute>()).ToArray();
	    foreach (TEntity entity in entitiesToUpdate)
	    {
		object[] primaryKeyValues = primaryKeyProperties
		    .Select(c => c.GetValue(entity)).ToArray();
		SqlParameter[] primaryKeyParameters = primaryKeyProperties
		    .Zip(primaryKeyValues, (param, value) => new SqlParameter(param.Name, value)).ToArray();
		object[] rowValues = columnsToUpdate
		    .Select(c => entity.GetType().GetProperty(c).GetValue(entity) ?? DBNull.Value).ToArray();
		SqlParameter[] columnsParameters = columnsToUpdate
		    .Zip(rowValues, (param, value) => new SqlParameter(param, value)).ToArray();
		string columnsSql = String.Join(", ", columnsToUpdate.Select(c => $"{c} = @{c}"));
		string primaryKeysSql = String.Join(" AND ",
		    primaryKeyProperties.Select(pk => $"{pk.Name} = @{pk.Name}"));
		string queryText = String.Format("UPDATE {0} SET {1} WHERE {2}", tableName, columnsSql, primaryKeysSql);
		int updatedRows = ExecuteNonQuery(queryText, columnsParameters.Concat(primaryKeyParameters).ToArray());
		if (updatedRows != 1)
		    throw new InvalidOperationException($"Update for table {tableName} failed.");
	    }
	}
    }
}
