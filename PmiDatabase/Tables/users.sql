CREATE TABLE users
(
	ID			int IDENTITY(1,1) PRIMARY KEY,
	FirstName	varchar(255) NOT NULL, --add some other check ???
	LastName	varchar(255),
	Login		varchar(255) NOT NULL UNIQUE, 
	Email		varchar(255),
	Password	varchar(20) CHECK(LEN(Password) > 8) NOT NULL,
	[BirthDate]			DATE,
	Image_id	INT	FOREIGN KEY REFERENCES images(ID),
	VK_ID		VARCHAR(50) NOT NULL,
	FB_ID		VARCHAR(50) NOT NULL,
	CONSTRAINT	uc_SocialNetworkID UNIQUE (VK_ID, FB_ID)
)

