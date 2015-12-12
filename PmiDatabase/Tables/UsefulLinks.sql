CREATE TABLE [dbo].[usefulLinks]
(
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255),
	[User_id] INT NOT NULL,
	[Url] VARCHAR(120) NOT NULL,
	[Image_Url] VARCHAR(MAX) NULL,
	[Comment] TEXT NULL
)
