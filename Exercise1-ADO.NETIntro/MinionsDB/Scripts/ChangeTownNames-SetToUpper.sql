USE MinionsDB

UPDATE Towns
SET [Name] = UPPER([Name])
WHERE CountryCode = @countryCode