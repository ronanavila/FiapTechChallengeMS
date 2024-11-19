USE TechChallenge
GO

CREATE TABLE dbo.Region(
	[DDD] int NOT NULL,
	[Location] varchar(100) NOT NULL,

    CONSTRAINT PK_Region PRIMARY KEY ([DDD])
)
GO

CREATE TABLE dbo.Contact(
	[Guid] UNIQUEIDENTIFIER NOT NULL,
    [Name] VARCHAR(150) NOT NULL,
    [Email] VARCHAR(150) NOT NULL,
    [Phone] VARCHAR(10) NOT NULL,
    [RegionDDD] INT NOT NULL,
    
    CONSTRAINT PK_Contact PRIMARY KEY ([Guid]),
    CONSTRAINT FK_Contact_Region FOREIGN KEY ([RegionDDD]) REFERENCES dbo.Region([DDD])     
)
GO
