CREATE TABLE events
(
	ID				int IDENTITY(1,1) PRIMARY KEY,
	Name			text, 
	Location		varchar(200),
	Description		text,
	Image_id		int FOREIGN KEY REFERENCES images(ID),
	Organizer_id	int FOREIGN KEY REFERENCES users(ID),
	Event_time		datetime
)