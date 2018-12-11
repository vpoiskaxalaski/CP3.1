use hotel
GO
DECLARE 
@day int,
@month int,
@year int,
@date datetime,
@room smallint
DECLARE curs cursor local for
SELECT RESERVATION.ROOM_NUMBER, RESERVATION.LEAVING FROM RESERVATION
BEGIN
	SET @day = DAY(GETDATE())
	SET @month = MONTH(GETDATE())
	SET @year = YEAR(GETDATE())
	OPEN curs
	FETCH NEXT FROM curs INTO @room, @date
	BEGIN
		WHILE @@FETCH_STATUS = 0 
			BEGIN
				IF @day = DAY(@date) AND @month = MONTH(@date) AND @year = YEAR(@date)
					UPDATE ROOM SET ISAVALIABLE = 1 WHERE ROOM_NUMBER = @room
				FETCH NEXT FROM curs INTO @room, @date
			END
	END
END
