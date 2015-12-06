CREATE TABLE [dbo].[comments]
(
	[Id]			INT NOT NULL PRIMARY KEY,
	Event_id		int NOT NULL,
	Author_id		int NOT NULL,
	Comment_time	DateTime NOT NULL,
	Comment			text NOT NULL
)
