CREATE PROCEDURE [dbo].[CreateUser]
	@email NVARCHAR(100),
	@password NVARCHAR(256),
	@salt NVARCHAR(50)
AS
	INSERT INTO Users ([Email], [Password], [Salt])
	VALUES (@email, @password, @salt)
GO