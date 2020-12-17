SELECT *
FROM Department;

SELECT *
FROM [User];

INSERT INTO Department (Name, DeanId) VALUES ('Computer Science', 1000);
INSERT INTO Department (Name, DeanId) VALUES ('English', 1001);
INSERT INTO Department (Name, DeanId) VALUES ('Engineering', 1002);
INSERT INTO Department (Name, DeanId) VALUES ('Arts', 1000);

ALTER TABLE Department DROP CONSTRAINT FK_DepartmentDean_Instructor;
ALTER TABLE Department ADD CONSTRAINT FK_DepartmentDean_Instructor FOREIGN KEY (DeanId) REFERENCES Instructor (InstructorId);

SELECT * FROM Instructor;