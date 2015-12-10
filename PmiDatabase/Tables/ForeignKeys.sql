ALTER TABLE [Users]
ADD CONSTRAINT [FK_Avatar_id]
FOREIGN KEY ([Image_id])
REFERENCES [Images]([ID])
GO
ALTER TABLE [ExternalLogins]
ADD CONSTRAINT [FK_External_Logins_User_Id]
FOREIGN KEY ([User_id])
REFERENCES [Users]([ID])
GO
ALTER TABLE [Events]
ADD CONSTRAINT [FK_Event_Image_id]
FOREIGN KEY ([Image_id])
REFERENCES [Images]([ID])
GO
ALTER TABLE [Events]
ADD CONSTRAINT [FK_Organizer_id]
FOREIGN KEY ([Organizer_id])
REFERENCES [Users]([ID])
GO
ALTER TABLE [Comments]
ADD CONSTRAINT [FK_Author_id]
FOREIGN KEY ([Author_id])
REFERENCES [Users]([ID])
GO
ALTER TABLE [Comments]
ADD CONSTRAINT [FK_Event_id]
FOREIGN KEY ([Event_id])
REFERENCES [Events]([ID])
GO
ALTER TABLE [usefulLinks]
ADD CONSTRAINT [FK_Useful_Links_User_id]
FOREIGN KEY ([User_id])
REFERENCES [Users]([ID])
GO
ALTER TABLE [usefulLinks]
ADD CONSTRAINT [FK_Useful_Links_Image_id]
FOREIGN KEY ([Image_id])
REFERENCES [Images]([ID])
GO
ALTER TABLE [messages]
ADD CONSTRAINT [FK_MessageAuthor_Id]
FOREIGN KEY ([AuthorId])
REFERENCES [Users]([ID])
GO
ALTER TABLE [messages]
ADD CONSTRAINT [FK_MessageRecipient_Id]
FOREIGN KEY ([RecipientId])
REFERENCES [Users]([ID])

