/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO images 
VALUES( 'https://i.ytimg.com/vi/GlcaoHxi5p4/maxresdefault.jpg' );

SET IDENTITY_INSERT [dbo].[users] ON
INSERT INTO [dbo].[users] ([ID], [FirstName], [LastName], [Login], [Email], [Password], [BirthDate], [Image_id], [VK_ID], [FB_ID]) VALUES (1005, N'Name', N'Lame', N'looogian', N'lal@mail.co.uk', N'qwerty123', N'1990-10-10', 1, N'1', N'1')
SET IDENTITY_INSERT [dbo].[users] OFF
