SELECT *
FROM Department;

INSERT INTO Department (Name, DeanId) VALUES ('Computer Science', 1);
INSERT INTO Department (Name, DeanId) VALUES ('English', 1);
INSERT INTO Department (Name, DeanId) VALUES ('Engineering', 1);
INSERT INTO Department (Name, DeanId) VALUES ('Arts', 1);

ALTER TABLE Department DROP CONSTRAINT FK_DepartmentDean_Instructor;
ALTER TABLE Department ADD CONSTRAINT FK_DepartmentDean_Instructor FOREIGN KEY (DeanId) REFERENCES Instructor (InstructorId);

SELECT * FROM Instructor;