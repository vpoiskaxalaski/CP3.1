USE hotel;
GO
DECLARE 
clientServ_curs CURSOR FOR SELECT CLIENT_ID FROM RESERVATION WHERE 
													DATENAME(day, LEAVING) = DATENAME(day, GETDATE()) AND
													DATENAME(month, LEAVING) = DATENAME(month, GETDATE()) AND
													DATENAME(year, LEAVING) = DATENAME(year, GETDATE()) 
DECLARE
@client int
OPEN clientServ_curs
FETCH NEXT FROM clientServ_curs INTO @client
WHILE @@FETCH_STATUS = 0
BEGIN
	UPDATE CLIENTSERVICE SET ACTUAL = 0 
		WHERE CLIENT_ID = @client
	FETCH NEXT FROM clientServ_curs INTO @client
END
CLOSE clientServ_curs	
