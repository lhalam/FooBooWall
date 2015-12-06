CREATE TABLE [dbo].[Verification]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Code] INT NOT NULL, 
    [Email] VARCHAR(255) NOT NULL, 
    [Verified] BIT NOT NULL
)
