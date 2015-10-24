CREATE TABLE users
(
	ID			int IDENTITY(1,1) PRIMARY KEY,
	FirstName	varchar(255) NOT NULL, --add some other check ???
	LastName	varchar(255),
	Login		varchar(255) NOT NULL, 
	Email		varchar(255),
	Password	varchar(20) CHECK(LEN(Password) > 8) NOT NULL,
	Age			int,
	Image_id	int	FOREIGN KEY REFERENCES images(ID),
	VK_ID		int NOT NULL,
	FB_ID		int NOT NULL,
	CONSTRAINT	uc_SocialNetworkID UNIQUE (VK_ID, FB_ID)
)

