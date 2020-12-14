SELECT * FROM Class;
SELECT * FROM Instructor;
SELECT * FROM Department;
SELECT * FROM Building;

INSERT INTO Class (InstructorId, DepartmentId, CourseName, CourseDescription, CourseCapacity, StartTime, EndTime, BuildingId, RoomNumber)
VALUES (1,9,'CS101', 'Intro To Computer Science', 150, '08/24/2020', '12/11/2020', 1, 200);

INSERT INTO Class (InstructorId, DepartmentId, CourseName, CourseDescription, CourseCapacity, StartTime, EndTime, BuildingId, RoomNumber)
VALUES (2,10,'ENG101', 'Intro To English', 50, '08/24/2020', '12/11/2020', 2, 201);

INSERT INTO Class (InstructorId, DepartmentId, CourseName, CourseDescription, CourseCapacity, StartTime, EndTime, BuildingId, RoomNumber)
VALUES (3,11,'E401', 'Advanced Engineering', 25, '08/24/2020', '12/11/2020', 1, 404);

INSERT INTO Class (InstructorId, DepartmentId, CourseName, CourseDescription, CourseCapacity, StartTime, EndTime, BuildingId, RoomNumber)
VALUES (1,12,'ARTS301', 'Intro In To Teapot Creating', 10, '08/24/2020', '12/11/2020', 1, 418);