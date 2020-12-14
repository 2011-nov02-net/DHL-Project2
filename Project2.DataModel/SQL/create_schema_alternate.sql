-- schema to based on https://dbdiagram.io/d/5fd67b429a6c525a03bae5d2

DROP TABLE IF EXISTS [users]
CREATE TABLE [users] (
  [id] guid PRIMARY KEY DEFAULT (newguid()),
  [full_name] nvarchar(120),
  [email] varchar(100),
  [permission] int
)
GO

DROP TABLE IF EXISTS 
CREATE TABLE [permissions] (
  [code] int PRIMARY KEY IDENTITY(1, 1),
  [name] varchar(20)
)
GO

DROP TABLE IF EXISTS  [buildings]
CREATE TABLE [buildings] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(120)
)
GO

DROP TABLE IF EXISTS [rooms]
CREATE TABLE [rooms] (
  [building] int FOREIGN KEY REFERENCES [rooms]([id]) ON DELETE CASCADE,
  [number] varchar(5),
  [id] int UNIQUE IDENTITY(1, 1),
  [capacity] int,
  PRIMARY KEY ([building], [number])
)
GO

DROP TABLE IF EXISTS [departments]
CREATE TABLE [departments] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(50) UNIQUE
)
GO

DROP TABLE IF EXISTS [categories]
CREATE TABLE [categories] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] varchar(20) UNIQUE
)
GO

DROP TABLE IF EXISTS [courses]
CREATE TABLE [courses] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(50),
  [description] nvarchar(200),
  [credits] int,
  [department] int FOREIGN KEY REFERENCES [departments]([id]),
  [code] int NOT NULL,
  [session] int FOREIGN KEY REFERENCES [sessions]([id]) ON DELETE CASCADE,
  [category] int FOREIGN KEY REFERENCES [categories]([id]),
  [primary] int FOREIGN KEY REFERENCES [courses]([id]) ON DELETE CASCADE, -- THIS IS THE PARENT CLASS OF GROUP CLASSES LIKE THE LECTURE FOR A DISCUSSION
  [optional] bit NOT NULL DEFAULT (false),
  [capacity] int NOT NULL,
  [waitlist_capacity] int NOT NULL
)
GO

DROP TABLE IF EXISTS [instructors]
CREATE TABLE [instructors] (
  [instructor] guid FOREIGN KEY REFERENCES [users]([id]) ON DELETE CASCADE,
  [course] int FOREIGN KEY REFERENCES [courses]([id]) ON DELETE CASCADE,
  PRIMARY KEY ([instructor], [course])
)
GO

DROP TABLE IF EXISTS [sessions]
CREATE TABLE [sessions] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(50),
  [start] date NOT NULL,
  [end] date NOT NULL
)
GO

DROP TABLE IF EXISTS [reservations]
CREATE TABLE [reservations] (
  [course] int FOREIGN KEY REFERENCES [courses]([id]),
  [room] int FOREIGN KEY REFERENCES [rooms]([id]),
  [start] datetime,
  [end] datetime,
  PRIMARY KEY ([room], [start])
)
GO

DROP TABLE IF EXISTS [grades]
CREATE TABLE [grades] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [letter] varchar(2) NOT NULL,
  [value] int
)
GO

DROP TABLE IF EXISTS [enrollment]
CREATE TABLE [enrollment] (
  [user] guid FOREIGN KEY REFERENCES [courses]([id]),
  [course] int FOREIGN KEY REFERENCES [courses]([id]),
  [grade] int FOREIGN KEY REFERENCES [grades]([id]),
  PRIMARY KEY ([user], [course])
)
GO

DROP TABLE IF EXISTS [waitlist]
CREATE TABLE [waitlist] (
  [user] guid FOREIGN KEY REFERENCES [users]([id]) ,
  [course] int FOREIGN KEY REFERENCES [courses]([id]),
  [added] datetime NOT NULL DEFAULT GETDATE(),
  PRIMARY KEY ([user], [course])
)
GO

DROP TABLE IF EXISTS [assistants]
CREATE TABLE [assistants] (
  [assistant] guid FOREIGN KEY REFERENCES [users]([id]),
  [course] int FOREIGN KEY REFERENCES [courses]([id]),
  PRIMARY KEY ([assistant], [course])
)
GO

-- Add more complicated check constraints 
--
-- 

CREATE VIEW [Enrolled_Count] AS
select [course], count(*) as [num_enrolled] from [enrollment] group by [course];

CREATE FUNCTION count_enrolled ( @courseId int ) RETURNS int AS
BEGIN
    return (select (top 1) [num_enrolled] 
            from [Enrolled_Count] 
            where @courseId = [course] )
END;

-- the number enrolled in the class is no more than its capacity
ALTER TABLE [course]
ADD CONSTRAINT [Mx_capacity] 
CHECK ([capacity] <= count_enrolled([id]));



CREATE VIEW [Waitlisted_Count] AS
select [course], count(*) as [num_waitlisted] from [waitlist] group by [course];

CREATE FUNCTION count_waitlisted ( @courseId int ) RETURNS int AS
BEGIN
    return (select (top 1) [num_waitlisted] 
            from [Waitlisted_Count] 
            where @courseId = [course] )
END;

-- the number waitlisted in the class is no more than its waitlist capacity
ALTER TABLE [waitlist]
ADD CONSTRAINT [Mx_waitlist_capacity] 
CHECK ([waitlist_capacity] <= count_waitlisted([id]));

-- calculate waitlist positions
CREATE VIEW [waitlist_order] AS
select ROW_NUMBER() OVER(PARTITION BY [course] ORDER BY [added] ASC) AS [position], [user], [course]
FROM [waitlist]

-- remove from waitlist and place into course
CREATE PROCEDURE [Dequeue_waitlist] @courseId int, @userId int out AS 
BEGIN ATOMIC 
SET @userId = SELECT [user] FROM [waitlist_order] 
  WHERE [course] = @courseId AND [position] = 1;
DELETE FROM [waitlist_order] WHERE [course] = @courseId AND [position] = 1;
INSERT INTO [enrollment] ([user], [course]) VALUES (@userId, @courseId);
END;
-- TODO: add trigger for delete from enrollment