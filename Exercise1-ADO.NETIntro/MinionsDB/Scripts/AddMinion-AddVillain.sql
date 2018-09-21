USE MinionsDB

INSERT INTO Villains ([Name], EvilnessFactorId)
VALUES (@villainName, 4)

SELECT Id
FROM Villains
WHERE [Name] = @villainName