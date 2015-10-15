CREATE TABLE events
(
	ID			int IDENTITY(1,1) PRIMARY KEY,
	Image_id	int FOREIGN KEY REFERENCES images(ID),
	Text		text
)