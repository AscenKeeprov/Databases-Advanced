USE MinionsDB

INSERT INTO Towns ([Name], CountryCode)
VALUES (@townName, NULL)

SELECT Id
FROM Towns
WHERE [Name] = @townName