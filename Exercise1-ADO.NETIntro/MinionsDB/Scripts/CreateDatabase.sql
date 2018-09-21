IF NOT EXISTS
(
	SELECT name
	FROM master.dbo.sysdatabases
	WHERE name = 'MinionsDB'
)
CREATE DATABASE MinionsDB
ELSE THROW 51001, 'Database MinionsDB already exists!', 1;