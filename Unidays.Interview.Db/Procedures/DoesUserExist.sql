CREATE PROCEDURE [dbo].[DoesUserExist]
	@emailAddress NVARCHAR(100)
AS
	SELECT CASE WHEN EXISTS (
    SELECT *
    FROM Users
    WHERE Email = @emailAddress
)
THEN CAST(1 AS BIT)
ELSE CAST(0 AS BIT) END
