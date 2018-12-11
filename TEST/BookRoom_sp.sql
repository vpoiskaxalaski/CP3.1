USE hotel
SELECT * FROM CLIENT


DECLARE 
@room_number SMALLINT = 120,
@client_id INT = 1,
@come datetime = '01-12-2018',
@live datetime = '05-12-2018'

EXEC BookRoom_sp @room_number, @client_id, @come, @live

SELECT * FROM RESERVATION
SELECT * FROM ROOM WHERE ISAVALIABLE = 0
DELETE FROM RESERVATION

SELECT * FROM CLIENTSERVICE

declare 
@mail VARCHAR(50) = 'dima@mail.ru'
select dbo.GetUserId (@mail)


exec GetRegisteredUsers_sp
