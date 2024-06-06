CREATE PROCEDURE [dbo].[spCustomer_Insert]
	@FirstName varchar(15),
	@LastName varchar(15),
	@Id int output
AS
BEGIN
	IF EXISTS (Select [Id] FROM dbo.Customer WHERE [FirstName]=@FirstName AND [LastName]=@LastName)
	BEGIN
		SELECT @Id = [Id] 
			FROM dbo.Customer 
			WHERE [FirstName]=@FirstName AND [LastName]=@LastName;
		
		return 1;
	END
	ELSE IF NOT EXISTS(Select [Id] from dbo.Customer WHERE [FirstName]=@FirstName AND [LastName]=@LastName)
	BEGIN
		INSERT INTO dbo.Customer
		([FirstName], [LastName])
		VALUES (@FirstName, @LastName);

		SET @Id = SCOPE_IDENTITY();

		return 1;
	END
	ELSE
	BEGIN
		SET @Id = 0;
		return 0;
	END
END
