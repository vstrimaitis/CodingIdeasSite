CREATE TRIGGER User_On_Null_Username ON [dbo].[User]
AFTER INSERT, UPDATE
AS
	DECLARE @username nvarchar(100), @id uniqueidentifier
	SELECT @username = Username FROM inserted
	SELECT @id = Id FROM inserted
	IF @username IS NULL
	BEGIN
		UPDATE [dbo].[User]
		SET Username = 'user' + CAST(@id AS nchar(64))
		WHERE Id = @id
	END