ALTER TABLE [Users]
ADD CONSTRAINT [FK_Avatar_id]
FOREIGN KEY ([Image_id])
REFERENCES [Images]([ID])
GO
ALTER TABLE [ExternalLogins]
ADD CONSTRAINT [FK_ExternalLogins_Users]
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