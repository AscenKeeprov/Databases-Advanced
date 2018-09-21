CREATE DATABASE MiniORM
GO

USE MiniORM
GO

CREATE TABLE Projects
(
	Id INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Departments
(
	Id INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Employees
(
	Id INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(50) NOT NULL,
	MiddleName VARCHAR(50),
	LastName VARCHAR(50) NOT NULL,
	IsEmployed BIT NOT NULL,
	DepartmentId INT
		CONSTRAINT FK_Employees_Departments
		FOREIGN KEY (DepartmentId)
		REFERENCES Departments(Id)
)
CREATE TABLE EmployeesProjects
(
	ProjectId INT NOT NULL
		CONSTRAINT FK_Employees_Projects
		FOREIGN KEY (ProjectId)
		REFERENCES Projects(Id),
	EmployeeId INT NOT NULL
		CONSTRAINT FK_Employees_Employee
		FOREIGN KEY (EmployeeId)
		REFERENCES Employees(Id),
	CONSTRAINT PK_Projects_Employees
	PRIMARY KEY (ProjectId, EmployeeId)
)
GO

INSERT INTO MiniORM.dbo.Projects ([Name]) VALUES
('C# Project'),
('Java Project')
GO

INSERT INTO MiniORM.dbo.Departments ([Name]) VALUES
('Accounting'),
('Quality Assurance'),
('Research and Development')
GO

INSERT INTO MiniORM.dbo.Employees
(
	FirstName, MiddleName, LastName, IsEmployed, DepartmentId
) VALUES
('Stamat', NULL, 'Ivanov', 1, 3),
('Petar', 'Ivanov', 'Petrov', 0, 1),
('Ivan', 'Petrov', 'Georgiev', 1, 3),
('Gosho', NULL, 'Ivanov', 1, 1)
GO

INSERT INTO MiniORM.dbo.EmployeesProjects (ProjectId, EmployeeId) VALUES
(1, 1),
(1, 3),
(2, 2),
(2, 3)
