USE hotel
GO
CREATE OR ALTER TRIGGER ReservationInsert
ON RESERVATION
AFTER INSERT
AS
UPDATE hotel.dbo.ROOM
    SET ISAVALIABLE = 0
    WHERE  ROOM_NUMBER = (SELECT ROOM_NUMBER FROM RESERVATION WHERE ID = @@IDENTITY)
