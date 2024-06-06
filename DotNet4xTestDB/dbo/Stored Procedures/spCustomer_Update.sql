CREATE PROCEDURE [dbo].[spCustomer_Update]
	@Id int,
	@FirstName nvarchar(15),
	@LastName nvarchar(15)
AS
BEGIN
	SET NOCOUNT OFF;

	UPDATE dbo.Customer
	SET [FirstName]=@FirstName, [LastName]=@LastName
	WHERE [Id]=@Id;
END