﻿CREATE PROCEDURE [dbo].[spCustomer_GetAll]
	
AS
BEGIN
	SELECT * 
	FROM dbo.Customer
	ORDER BY [LastName], [FirstName];
END
