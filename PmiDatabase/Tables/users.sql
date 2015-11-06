﻿CREATE TABLE users
(
	ID			int IDENTITY(1,1) PRIMARY KEY,
	FirstName	varchar(255) NULL, --add some other check ???
	LastName	varchar(255),
	Login		varchar(255) NOT NULL UNIQUE, 
	Email		varchar(255) NOT NULL,
	PasswordHash	varchar(MAX) NULL,
	[BirthDate]			DATE,
	Image_id	INT	FOREIGN KEY REFERENCES images(ID),
	VK_ID		VARCHAR(50) NULL,
	FB_ID		VARCHAR(50) NULL,
	[SecurityStamp] VARCHAR(MAX) NULL, 
    CONSTRAINT	uc_SocialNetworkID UNIQUE (VK_ID, FB_ID)
)

