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

INSERT INTO images (Name)
VALUES('https://i.ytimg.com/vi/GlcaoHxi5p4/maxresdefault.jpg' );
INSERT INTO images (Name)
VALUES('https://pp.vk.me/c628627/v628627737/a321/2uk5TUqgz6E.jpg' );

INSERT INTO users(FirstName, LastName, Login, Email, BirthDate, Image_id)
VALUES('John', 'McKein', 'YourTree', 'mcKeinTheKing@ukr.net', '1995-01-03', 1);

INSERT INTO users(FirstName, LastName, Login, Email, BirthDate, Image_id)
VALUES('Sofiya', 'Padus', 'YourQeen', 'sofija_padus@ukr.net', '1996-12-04', 2);