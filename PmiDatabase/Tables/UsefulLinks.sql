﻿CREATE TABLE [dbo].[usefulLinks]
(
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[OwnerUserId] INT FOREIGN KEY REFERENCES users(ID) NOT NULL,
	[Url] VARCHAR(120) NOT NULL,
	[ImageId] INT FOREIGN KEY REFERENCES images(ID) NULL,
	[Comment] TEXT NULL
)