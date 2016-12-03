CREATE UNIQUE INDEX User_Email 	ON [dbo].[User](Email);
CREATE INDEX User_Username 		ON [dbo].[User](Username);
CREATE INDEX Post_Title 		ON [dbo].Post(Title);