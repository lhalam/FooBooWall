CREATE TABLE [dbo].[comments]
(
	[Id]			INT NOT NULL PRIMARY KEY,
	Event_id		int FOREIGN KEY REFERENCES events(ID) NOT NULL,
	Author_id		int FOREIGN KEY REFERENCES users(ID) NOT NULL,
	Comment_time	DateTime NOT NULL,
	Comment			text NOT NULL
)
