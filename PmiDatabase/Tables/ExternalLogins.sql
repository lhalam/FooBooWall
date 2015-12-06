CREATE TABLE [dbo].[ExternalLogins]
(
    [ID] INT Identity(1, 1) NOT NULL PRIMARY KEY, 
    [User_id] INT NOT NULL DEFAULT 0, 
    [LoginProvider] VARCHAR(MAX) NOT NULL DEFAULT '', 
    [ProviderKey] VARCHAR(MAX) NOT NULL DEFAULT ''
)
