CREATE TABLE [dbo].[externalLogins]
(
    [ID] INT Identity(1, 1) NOT NULL PRIMARY KEY, 
    [User_id] INT NOT NULL, 
    [LoginProvider] VARCHAR(MAX) NOT NULL, 
    [ProviderKey] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_ExternalLogins_Users] FOREIGN KEY ([User_id]) REFERENCES [users]([ID])
)
