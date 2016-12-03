CREATE TABLE [dbo].Programming_Language
(
    Id      uniqueidentifier  NOT NULL DEFAULT NEWID(),
    Name    VARCHAR(15)       NOT NULL,
    
    PRIMARY KEY (Id)
);

CREATE TABLE [dbo].[User]
(
    Id          uniqueidentifier          NOT NULL    DEFAULT NEWID(),
    Email       VARCHAR(254)    NOT NULL,
    Username    NVARCHAR(100),
    First_Name  NVARCHAR(30),
    Last_Name   NVARCHAR(30),
    Password    CHAR(64) 	NOT NULL, -- use SHA-256
    DOB         DATE                        CONSTRAINT CK_User_DOB
                                            CHECK (DOB <= DATEADD(year, -5, GETDATE())),
    
    PRIMARY KEY (Id)
);

CREATE TABLE [dbo].User_Skills
(
    User_Id                 uniqueidentifier     NOT NULL,
    Programming_Language_Id uniqueidentifier     NOT NULL,
    Proficiency             SMALLINT    NOT NULL    CONSTRAINT CK_UserSkills_Proficiency
                                                    CHECK (Proficiency >= 0 AND Proficiency <= 5),
                                            
    PRIMARY KEY (User_Id, Programming_Language_Id),
    FOREIGN KEY (User_Id)                   REFERENCES [dbo].[User]
                                            ON DELETE CASCADE
                                            ON UPDATE NO ACTION,
    FOREIGN KEY (Programming_Language_Id)   REFERENCES Programming_Language
                                            ON DELETE CASCADE
                                            ON UPDATE NO ACTION
);

CREATE TABLE [dbo].Ratable_Entity
(
    Id              uniqueidentifier      NOT NULL DEFAULT NEWID(),
    User_Id         uniqueidentifier,
    Publish_Date    DATE        NOT NULL    CONSTRAINT CK_RatableEntity_PublishDate
                                            CHECK (Publish_Date <= GETDATE()),
    
    PRIMARY KEY (Id),
    
    FOREIGN KEY (User_Id) REFERENCES [dbo].[User]
                          ON DELETE SET NULL -- If a [User] is deleted, don't delete the post
                          ON UPDATE NO ACTION
);

CREATE TABLE [dbo].Rated_Entities
(
    Entity_Id       uniqueidentifier     NOT NULL,
    User_Id         uniqueidentifier     NOT NULL,
    Rating          SMALLINT    NOT NULL    CONSTRAINT CK_RatedEntities_Rating
                                            CHECK (Rating = -1 OR Rating = 1),
                                            
    PRIMARY KEY (Entity_Id, User_Id),
    FOREIGN KEY (Entity_Id) REFERENCES Ratable_Entity
                            ON DELETE CASCADE
                            ON UPDATE NO ACTION,
    FOREIGN KEY (User_Id)   REFERENCES [dbo].[User]
                            ON DELETE CASCADE
                            ON UPDATE NO ACTION
);

CREATE TABLE [dbo].Post
(
    Id              uniqueidentifier     	NOT NULL,
    Title           NVARCHAR(100)    NOT NULL,
    Content         NVARCHAR(MAX)            NOT NULL, 
    
    PRIMARY KEY (Id),
    FOREIGN KEY (Id) REFERENCES Ratable_Entity
                     ON DELETE CASCADE
                     ON UPDATE NO ACTION
);

CREATE TABLE [dbo].Saved_Posts
(
    Post_Id     uniqueidentifier     NOT NULL,
    User_Id     uniqueidentifier     NOT NULL,
    
    PRIMARY KEY (Post_Id, User_Id),
    FOREIGN KEY (Post_Id) REFERENCES Post
                          ON DELETE CASCADE
                          ON UPDATE NO ACTION,
    FOREIGN KEY (User_Id) REFERENCES [dbo].[User]
                          ON DELETE CASCADE
                          ON UPDATE NO ACTION
);

CREATE TABLE [dbo].Comment
(
    Id          uniqueidentifier         NOT NULL,
    Post_Id     uniqueidentifier         NOT NULL,
    Content     VARCHAR(1024)   NOT NULL,
    
    PRIMARY KEY (Id),
    FOREIGN KEY (Id)        REFERENCES Ratable_Entity
                            ON DELETE CASCADE
                            ON UPDATE NO ACTION,
    FOREIGN KEY (Post_Id)   REFERENCES Post
                            ON DELETE NO ACTION
                            ON UPDATE NO ACTION
);
