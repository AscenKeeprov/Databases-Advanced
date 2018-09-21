USE MinionsDB

IF  NOT EXISTS
(
	SELECT * FROM sys.objects
	WHERE name = 'Countries'
	AND type = 'U'
)
BEGIN
	CREATE TABLE Countries
	(
		Id INT IDENTITY
			CONSTRAINT PK__Country_Id
			PRIMARY KEY (Id),
		[Name] VARCHAR(64) NOT NULL
	)
END

IF  NOT EXISTS
(
	SELECT * FROM sys.objects
	WHERE name = 'Towns'
	AND type = 'U'
)
BEGIN
	CREATE TABLE Towns
	(
		Id INT IDENTITY
			CONSTRAINT PK__Town_Id
			PRIMARY KEY (Id),
		[Name] VARCHAR(64) NOT NULL,
		CountryCode INT
			CONSTRAINT FK__Towns_CountryCode__Countries_Id
			FOREIGN KEY (CountryCode) REFERENCES Countries(Id)
	)
END

IF  NOT EXISTS
(
	SELECT * FROM sys.objects
	WHERE name = 'Minions'
	AND type = 'U'
)
BEGIN
	CREATE TABLE Minions
	(
		Id INT IDENTITY
			CONSTRAINT PK__Minion_Id
			PRIMARY KEY (Id),
		[Name] VARCHAR(32) NOT NULL,
		Age INT,
		TownId INT
			CONSTRAINT FK__Minions_TownId__Towns_Id
			FOREIGN KEY REFERENCES Towns(Id)
	)
END

IF  NOT EXISTS
(
	SELECT * FROM sys.objects
	WHERE name = 'EvilnessFactors'
	AND type = 'U'
)
BEGIN
	CREATE TABLE EvilnessFactors
	(
		Id INT IDENTITY
			CONSTRAINT PK__EvilnessFactor_Id
			PRIMARY KEY (Id),
		[Name] VARCHAR(64) NOT NULL
	)
END

IF  NOT EXISTS
(
	SELECT * FROM sys.objects
	WHERE name = 'Villains'
	AND type = 'U'
)
BEGIN
	CREATE TABLE Villains
	(
		Id INT PRIMARY KEY IDENTITY,
		[Name] VARCHAR(64) NOT NULL,
		EvilnessFactorId INT
			CONSTRAINT FK__Villains_EvilnessFactor__EvilnessFactors_Id
			FOREIGN KEY (EvilnessFactorId) REFERENCES EvilnessFactors(Id)
	)
END

IF  NOT EXISTS
(
	SELECT * FROM sys.objects
	WHERE name = 'MinionsVillains'
	AND type = 'U'
)
BEGIN
	CREATE TABLE MinionsVillains
	(
		MinionId INT NOT NULL
			CONSTRAINT FK__MinionsVillains_MinionId__Minions_Id
			FOREIGN KEY (MinionId) REFERENCES Minions(Id),
		VillainId INT NOT NULL
			CONSTRAINT FK__MinionsVillains_VillainId__Villains_Id
			FOREIGN KEY (VillainId) REFERENCES Villains(Id),
		CONSTRAINT PK_MinionsVillains_MinionId_VillainId
		PRIMARY KEY (MinionId, VillainId)
	)
END