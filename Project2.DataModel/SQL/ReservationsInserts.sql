SELECT *
FROM Reservation;

SELECT *
FROM Room;

SELECT * FROM COURSE;

INSERT INTO Reservation (Room, CourseId, Start, [End]) VALUES
	(68, 1 , '12:00:00', '13:30:00'),
	(68, 1 , '13:45:00', '15:15:00'),
	(78, 7 , '09:00:00', '10:30:00'),
	(94, 7 , '12:00:00', '13:30:00'),
	(81, 13 , '12:00:00', '15:00:00'),
	(73, 13 , '09:00:00', '12:00:00'),
	(70, 19 , '12:00:00', '14:00:00'),
	(79, 19 , '10:30:00', '12:30:00')