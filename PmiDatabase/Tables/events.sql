CREATE TABLE [dbo].[Events]
(
	ID				int IDENTITY(1,1) PRIMARY KEY,
	Name			text DEFAULT '', 
	Location		varchar(200) DEFAULT '',
	Description		text DEFAULT '',
	Event_time		datetime DEFAULT ''
)