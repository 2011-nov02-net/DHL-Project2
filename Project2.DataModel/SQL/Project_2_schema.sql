DROP TABLE IF EXISTS [User]
CREATE TABLE [User] (
  Id INT IDENTITY(1000,1) PRIMARY KEY,
  Full_Name NVARCHAR(120),
  Email VARCHAR(100) UNIQUE,
  Permission INT
);

DROP TABLE IF EXISTS Permission;
CREATE TABLE Permission (
	Code INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(20)
);

DROP TABLE IF EXISTS Building;
CREATE TABLE Building (
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(120)
);

DROP TABLE IF EXISTS Room;
CREATE TABLE Room (
	Id INT UNIQUE IDENTITY,
	Number DECIMAL(5,0),
	Capacity INT CHECK(Capacity > 0),
	BuildingId INT,
	PRIMARY KEY (Number, BuildingId)
);

DROP TABLE IF EXISTS Department;
CREATE TABLE Department (
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(50) UNIQUE,
	DeanId INT
)

DROP TABLE IF EXISTS Category;
CREATE TABLE Category (
	Id INT PRIMARY KEY IDENTITY,
	Name VARCHAR(20) UNIQUE
);

DROP TABLE IF EXISTS Session
CREATE TABLE Session (
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(50),
	Start DATE NOT NULL,
	[End] DATE NOT NULL
);

DROP TABLE IF EXISTS Reservation
CREATE TABLE Reservation (
	Room INT,
	CourseId Int,
	Start TIME,
	[End] TIME,
	PRIMARY KEY (Room, Start)
)

DROP TABLE IF EXISTS Course;
CREATE TABLE Course (
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(50) UNIQUE,
	Description NVARCHAR(200),
	CreditValue INT,
	DepartmentId INT,
	Code INT,
	Session INT,
	Category INT,
	Capacity INT,
	WaitlistCapacity INT
)

DROP TABLE IF EXISTS Instructor
CREATE TABLE Instructor (
	InstructorId INT,
	CourseId INT,
	PRIMARY KEY (InstructorId, CourseId)
)

DROP TABLE IF EXISTS CourseAssistant
CREATE TABLE CourseAssistant (
	AssistantId INT,
	CourseId Int,
	Role NVARCHAR(15) CHECK(Role in ('UTA', 'GTA', 'TA'))
	PRIMARY KEY (AssistantId, CourseId)
)

DROP TABLE IF EXISTS Waitlist
CREATE TABLE Waitlist (
	[User] INT,
	CourseId INT,
	Added DATETIME,
	PRIMARY KEY ([User], CourseId)
)

DROP TABLE IF EXISTS Enrollment
CREATE TABLE Enrollment (
	[User] INT,
	Course INT,
	Grade Int,
	PRIMARY KEY ([User], Course)
)

DROP TABLE IF EXISTS Grade
CREATE TABLE Grade (
	Id INT PRIMARY KEY IDENTITY,
	Letter NVARCHAR(2) CHECK(Letter IN('A', 'B', 'C', 'D', 'F', 'IC', 'P', 'U')),
	Value INT
)
GO;

ALTER TABLE Room ADD CONSTRAINT FK_RoomBuildingId_BuildingId FOREIGN KEY (BuildingId) REFERENCES Building(Id) ON DELETE CASCADE;

ALTER TABLE Reservation ADD CONSTRAINT FK_ReservationRoom_RoomId FOREIGN KEY (Room) REFERENCES Room (Id);
ALTER TABLE Reservation ADD CONSTRAINT FK_ReservationCourse_CourseId FOREIGN KEY (CourseId) REFERENCES Course (Id);

ALTER TABLE Department ADD CONSTRAINT FK_DepartmentDeanId_UserId FOREIGN KEY (DeanId) REFERENCES [User] (Id);

ALTER TABLE Course ADD CONSTRAINT FK_CourseDepartmentId_DepartmentId FOREIGN KEY (DepartmentId) REFERENCES Department (Id);
ALTER TABLE Course ADD CONSTRAINT FK_CourseSession_SessionId FOREIGN KEY (Session) REFERENCES Session (Id) ON DELETE CASCADE;
ALTER TABLE Course ADD CONSTRAINT FK_CourseCategory_CategoryId FOREIGN KEY(Category) REFERENCES Category (Id);

ALTER TABLE Instructor ADD CONSTRAINT FK_InstructorId_UserId FOREIGN KEY(InstructorId) REFERENCES [User] (Id) ON DELETE CASCADE;
ALTER TABLE Instructor ADD CONSTRAINT FK_InstructorCourseId_CourseId FOREIGN KEY (CourseId) REFERENCES Course (Id) ON DELETE CASCADE;

ALTER TABLE CourseAssistant ADD CONSTRAINT FK_CourseAssistantId_UserId FOREIGN KEY (AssistantId) REFERENCES [User] (Id);
ALTER TABLE CourseAssistant ADD CONSTRAINT FK_CourseAssistantCourseId_CourseId FOREIGN KEY (CourseId) REFERENCES Course (Id);

ALTER TABLE Waitlist ADD CONSTRAINT FK_WaitlistUserId_UserId FOREIGN KEY ([User]) REFERENCES [User] (Id);
ALTER TABLE Waitlist ADD CONSTRAINT FK_WaitlistCourseId_CourseId FOREIGN KEY (CourseId) REFERENCES Course (Id);

ALTER TABLE Enrollment ADD CONSTRAINT FK_EnrollmentUserId_UserId FOREIGN KEY ([User]) REFERENCES [User] (Id);
ALTER TABLE Enrollment ADD CONSTRAINT FK_EnrollmentCourseId_CourseId FOREIGN KEY (Course) REFERENCES Course (Id);
ALTER TABLE Enrollment ADD CONSTRAINT FK_EnrollmentGrade_GradeId FOREIGN KEY (Grade) REFERENCES Grade (Id);

ALTER TABLE [User] ADD CONSTRAINT FK_UserPermission_Permission FOREIGN KEY (Permission) REFERENCES Permission (Code);


-- EXTRA STUFF
CREATE VIEW [Enrolled_Count] AS
select [course], count(*) as [num_enrolled] from [enrollment] group by [course];

CREATE FUNCTION count_enrolled ( @courseId int ) RETURNS int AS
BEGIN
    return (select (top 1) [num_enrolled] 
            from [Enrolled_Count] 
            where @courseId = [course] )
END;

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

ALTER TABLE [waitlist]
ADD CONSTRAINT [Mx_waitlist_capacity] 
CHECK ([waitlist_capacity] <= count_waitlisted([id]));

CREATE VIEW [waitlist_order] AS
select ROW_NUMBER() OVER(PARTITION BY [course] ORDER BY [added] ASC) AS [position], [user], [course]
FROM [waitlist]

CREATE PROCEDURE [Dequeue_waitlist] @courseId int, @userId int out AS 
BEGIN ATOMIC 
SET @userId = SELECT [user] FROM [waitlist_order] 
  WHERE [course] = @courseId AND [position] = 1;
DELETE FROM [waitlist_order] WHERE [course] = @courseId AND [position] = 1;
INSERT INTO [enrollment] ([user], [course]) VALUES (@userId, @courseId);
END;