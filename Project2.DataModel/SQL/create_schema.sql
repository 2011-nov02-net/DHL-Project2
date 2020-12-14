-- based of the diageam: https://drive.google.com/file/d/1vbZ_E7XsWTnbINBwabt1uGdYjkSsvt09/view

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

-- Create a new table called '[Person]' in schema 'Project2'
-- Drop the table if it already exists
IF OBJECT_ID('Project2.[Person]', 'U') IS NOT NULL
DROP TABLE Project2.[Person]
GO
-- Create the table in the specified schema
CREATE TABLE Project2.[Person]
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), -- primary key column
    [Name] [NVARCHAR](120) NOT NULL,
    [Email] [VARCHAR](100) NOT NULL UNIQUE,
    [Role] [VARCHAR](10) NOT NULL 
);
GO

-- Create a new table called 'Building' in schema 'Project2'
-- Drop the table if it already exists
IF OBJECT_ID('Project2.Building', 'U') IS NOT NULL
DROP TABLE Project2.Building
GO
-- Create the table in the specified schema
CREATE TABLE Project2.Building
(
    Id INT IDENTITY PRIMARY KEY, -- primary key column
    [Name] [NVARCHAR](120) NOT NULL
);
GO

-- Create a new table called 'Instructor' in schema 'Project2'
-- Drop the table if it already exists
IF OBJECT_ID('Project2.Instructor', 'U') IS NOT NULL
DROP TABLE Project2.Instructor
GO
-- Create the table in the specified schema
CREATE TABLE Project2.Instructor
(
    InstructorId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY FOREIGN KEY 
        REFERENCES Project2.Person(Id) ON DELETE CASCADE, -- primary key column
    DepartmentId INT NOT NULL FOREIGN KEY REFERENCES Project2.Department(Id) ON DELETE CASCADE,
);
GO


-- Create a new table called 'Department' in schema 'Project2'
-- Drop the table if it already exists
IF OBJECT_ID('Project2.Department', 'U') IS NOT NULL
DROP TABLE Project2.Department
GO
-- Create the table in the specified schema
CREATE TABLE Project2.Department
(
    Id INT IDENTITY PRIMARY KEY, -- primary key column
    [Name] [NVARCHAR](120) NOT NULL,
    [DeanId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY 
        REFERENCES Project2.Instructor(InstructorId) ON DELETE CASCADE
);
GO

-- Create a new table called 'CourseAssistants' in schema 'Project2'
-- Drop the table if it already exists
IF OBJECT_ID('Project2.CourseAssistants', 'U') IS NOT NULL
DROP TABLE Project2.CourseAssistants
GO
-- Create the table in the specified schema
CREATE TABLE Project2.CourseAssistants
(
    AssistantId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY FOREIGN KEY 
        REFERENCES Project2.Person(Id) ON DELETE CASCADE, -- primary key column
    CourseId [NVARCHAR](50) NOT NULL,
    [Role] [NVARCHAR](10) NOT NULL
);
GO

-- Create a new table called 'Enrollment' in schema 'Project2'
-- Drop the table if it already exists
IF OBJECT_ID('Project2.Enrollment', 'U') IS NOT NULL
DROP TABLE Project2.Enrollment
GO
-- Create the table in the specified schema
CREATE TABLE Project2.Enrollment
(
    CourseId [NVARCHAR](50) NOT NULL,
    StudentId UNIQUEIDENTIFIER FOREIGN KEY
        REFERENCES Project2.Person(Id) ON DELETE CASCADE,
    CONSTRAINT pk_enrollment PRIMARY KEY (CourseId, StudentId)
);
GO

-- Create a new table called 'Transcript' in schema 'Project2'
-- Drop the table if it already exists
IF OBJECT_ID('Project2.Transcript', 'U') IS NOT NULL
DROP TABLE Project2.Transcript
GO
-- Create the table in the specified schema
CREATE TABLE Project2.Transcript
(
    PersonId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Project2.Person(Id) ON DELETE CASCADE,
    CourseId INT NOT NULL,
    Grade INT,
);
GO

-- Create a new table called 'Class' in schema 'Project2'
-- Drop the table if it already exists
IF OBJECT_ID('Project2.Class', 'U') IS NOT NULL
DROP TABLE Project2.Class
GO
-- Create the table in the specified schema
CREATE TABLE Project2.Class
(
    Id INT IDENTITY PRIMARY KEY, -- primary key column
    InstructorId UNIQUEIDENTIFIER FOREIGN KEY 
        REFERENCES Project2.Instructor(InstructorId) ON DELETE CASCADE,
    DepartmentId INT NOT NULL FOREIGN KEY 
        REFERENCES Project2.Department(Id) ON DELETE CASCADE,
    CourseName NVARCHAR(50) NOT NULL,
    CourseDescription NVARCHAR(500),
    CourseCapacity INT NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    BuildingId INT NOT NULL FOREIGN KEY
        REFERENCES Project2.Building(Id) ON DELETE CASCADE,
    RoomNumber INT NOT NULL
);
GO