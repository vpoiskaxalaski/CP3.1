USE hotel

DECLARE 
@i INT = 100,
@n INT,
@floor INT = 1
BEGIN TRY
	WHILE @floor < 6
	BEGIN
		IF @i = 130
			SET @i = 200
		ELSE IF @i = 230
			SET @i = 300
		ELSE IF @i = 330
			SET @i = 400
		ELSE IF @i = 430
			SET @i = 500
		SET @n = 1
		while @n > 0 AND @n < 11
		BEGIN
			INSERT INTO ROOM VALUES (@i, 'Одноместный', FLOOR(RAND()*100),1 )
			SET @i = @i + 1 
			SET @n = @n + 1
		END
		while @n > 10 AND @n < 21
		BEGIN
			INSERT INTO ROOM VALUES (@i, 'Двуместный', FLOOR(RAND()*1000), 1)
			SET @i = @i + 1 
			SET @n = @n + 1
		END
		while @n > 20 AND @n < 31
		BEGIN
			INSERT INTO ROOM VALUES (@i, 'Семейный', FLOOR(RAND()*10000), 1 )
			SET @i = @i + 1 
			SET @n = @n + 1
		END
		SET @floor = @floor + 1 
	END
END TRY
BEGIN CATCH
	PRINT 'Error ' + CONVERT(VARCHAR, ERROR_NUMBER()) + ':' + ERROR_MESSAGE()
END CATCH


DELETE  FROM ROOM
SELECT * FROM ROOM
SELECT COUNT(*) FROM ROOM