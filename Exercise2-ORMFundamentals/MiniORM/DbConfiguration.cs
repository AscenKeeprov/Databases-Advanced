namespace MiniORM
{
    using System;
    using System.Xml;

    public static class DbConfiguration
    {
	public const string ConnectionString = @"Server=.\SQLEXPRESS;Database=MiniORM;Integrated Security=true";

	internal static Type[] AllowedSqlTypes =
	{
	    typeof(Boolean),
	    typeof(Byte),
	    typeof(Byte[]),
	    typeof(Char[]),
	    typeof(DateTime),
	    typeof(Decimal),
	    typeof(Double),
	    typeof(Int16),
	    typeof(Int32),
	    typeof(Int64),
	    typeof(Single),
	    typeof(String),
	    typeof(TimeSpan),
	    typeof(XmlDocument)
	};
    }
}
