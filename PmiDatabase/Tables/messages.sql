CREATE TABLE [dbo].[messages]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [DateTime] DATETIME NOT NULL, 
    [Text] VARCHAR(MAX) NOT NULL, 
    [AuthorId] INT NOT NULL, 
    [RecipientId] INT NULL
)
