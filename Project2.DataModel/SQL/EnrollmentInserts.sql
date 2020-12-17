SELECT * FROM Enrollment;
SELECT * FROM [User];
SELECT * FROM Course;
INSERT INTO Enrollment ([User], Course, Grade) VALUES 
	(1009, 1, null),
	(1010, 1, null),
	(1011, 1, null),
	(1009, 7, null),
	(1010, 7, null),
	(1011, 7, null),
	(1009, 13, null),
	(1010, 13, null),
	(1011, 13, null),
	(1009, 19, null),
	(1010, 19, null),
	(1011, 19, null),
	(1012, 1, 1),
	(1013, 1, 4),
	(1014, 1, 3),
	(1012, 7, 6),
	(1013, 7, 2),
	(1014, 7, 4),
	(1012, 13, 5),
	(1013, 13, 2),
	(1014, 13, 2),
	(1012, 19, 3),
	(1013, 19, 1),
	(1014, 19, 2)