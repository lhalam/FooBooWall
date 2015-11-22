CREATE TABLE [dbo].[Users]
(
	ID			int IDENTITY(1,1) PRIMARY KEY,
	FirstName	varchar(255) NULL DEFAULT '', --add some other check ???
	LastName	varchar(255) DEFAULT '',
	Login		varchar(255) NOT NULL UNIQUE DEFAULT '', 
	Email		varchar(255) NOT NULL DEFAULT '',
	PasswordHash	varchar(MAX) NULL DEFAULT '',
	[BirthDate]			DATE DEFAULT '',
	[SecurityStamp] VARCHAR(MAX) NULL DEFAULT ''
)

