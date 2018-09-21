USE MinionsDB

IF NOT EXISTS
(
	SELECT *
	FROM MinionsVillains
	WHERE MinionId = @minionId
	AND VillainId = @villainId
)
BEGIN
	INSERT INTO MinionsVillains (MinionId, VillainId)
	VALUES (@minionId, @villainId)
END