DECLARE
@first_name nvarchar(30),
@mail varchar(50),
@password varbinary(256),
@PassphraseEnteredByUser nvarchar(128) = 'A little learning is a dangerous thing!',
@i int = 0
BEGIN
BEGIN TRANSACTION
	WHILE @i < 10000
	BEGIN
		SET @first_name = 'Client' + CONVERT(nvarchar(4), @i)
		SET @mail = @first_name +'@mail.ru'
		SET @password = EncryptByPassPhrase(@PassphraseEnteredByUser, @first_name )
		INSERT INTO CLIENT VALUES(@first_name, @mail, @password)
		SET @i = @i + 1;
		IF @@ERROR <> 0
			ROLLBACK
	END
COMMIT
END


SELECT COUNT (*) FROM CLIENT