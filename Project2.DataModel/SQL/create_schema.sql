-- Create a new database called 'Project2'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'Project2'
)
CREATE DATABASE Project2
GO

-- Create a new table called '[users]' in schema 'project2'
-- Drop the table if it already exists
IF OBJECT_ID('project2.[users]', 'U') IS NOT NULL
DROP TABLE project2.[users]
GO
-- Create the table in the specified schema
CREATE TABLE project2.[users]
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), -- primary key column
    First_Name [NVARCHAR](50) NOT NULL,
    Last_Name [NVARCHAR](50) NOT NULL,
    Email [varchar](50) NOT NULL UNIQUE
);
GO

-- Create a new table called 'buildings' in schema 'Project2'
-- Drop the table if it already exists
IF OBJECT_ID('Project2.buildings', 'U') IS NOT NULL
DROP TABLE Project2.buildings
GO
-- Create the table in the specified schema
CREATE TABLE Project2.buildings
(
    buildingsId INT IDENTITY PRIMARY KEY, -- primary key column
    Name [NVARCHAR](50) NOT NULL
);
GO