﻿CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Email] NVARCHAR(100) NOT NULL,
	[Password] NVARCHAR(256) NOT NULL,
	[Salt] NVARCHAR(50) NOT NULL
)

GO

CREATE INDEX [IX_Users_Column] ON [dbo].[Users] ([Email])
