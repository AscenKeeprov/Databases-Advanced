USE MinionsDB

SELECT v.[Name], COUNT(m.Id)
FROM Villains AS v
JOIN MinionsVillains AS mv
ON mv.VillainId = v.Id
JOIN Minions AS m
ON m.Id = mv.MinionId
GROUP BY v.[Name]
HAVING COUNT(m.Id) > 3
ORDER BY COUNT(m.Id) DESC