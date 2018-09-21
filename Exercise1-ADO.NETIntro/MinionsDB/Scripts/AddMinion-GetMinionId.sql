USE MinionsDB

IF NOT EXISTS
(
	SELECT *
	FROM Minions
	WHERE [Name] = @minionName
	AND Age = @minionAge
	AND TownId = @townId
)
BEGIN
	INSERT INTO Minions ([Name], Age, TownId)
	VALUES (@minionName, @minionAge, @townId)
END

SELECT Id
FROM Minions
WHERE [Name] = @minionName
AND Age = @minionAge
AND TownId = @townId