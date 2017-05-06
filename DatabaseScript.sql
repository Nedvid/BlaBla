CREATE TABLE [dbo].[Users] (
    [Id_User]  INT           IDENTITY (1, 1) NOT NULL,
    [Login]    VARCHAR (45)  NOT NULL,
    [Password] VARCHAR (256) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_User] ASC)
);

CREATE TABLE [dbo].[Friendship] (
    [Id_Friendship]    INT IDENTITY (1, 1) NOT NULL,
    [Id_User1]         INT NOT NULL,
    [Id_User2]         INT NOT NULL,
    [FriendshipStatus] BIT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Friendship] ASC),
    CONSTRAINT [FK_Friendship_Users] FOREIGN KEY ([Id_User1]) REFERENCES [dbo].[Users] ([Id_User]),
    CONSTRAINT [FK_Friendship_Users2] FOREIGN KEY ([Id_User2]) REFERENCES [dbo].[Users] ([Id_User])
);

CREATE TABLE [dbo].[Messages] (
    [Id_Message]  INT  IDENTITY (1, 1) NOT NULL,
    [Id_Sender]   INT  NOT NULL,
    [Id_Receiver] INT  NOT NULL,
    [MessageData] TEXT NOT NULL,
    [SendDate]    DATE NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Message] ASC),
    CONSTRAINT [FK_Messages_Users] FOREIGN KEY ([Id_Sender]) REFERENCES [dbo].[Users] ([Id_User]),
    CONSTRAINT [FK_Messages_Users2] FOREIGN KEY ([Id_Receiver]) REFERENCES [dbo].[Users] ([Id_User])
);


CREATE TABLE [dbo].[VoiceHistory] (
    [Id_VoiceHistory] INT      IDENTITY (1, 1) NOT NULL,
    [Id_Caller]       INT      NOT NULL,
    [Id_Receiver]     INT      NOT NULL,
    [CallDate]        DATE     NOT NULL,
    [Duration]        TIME (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_VoiceHistory] ASC),
    CONSTRAINT [FK_VoiceHistory_Users] FOREIGN KEY ([Id_Caller]) REFERENCES [dbo].[Users] ([Id_User]),
    CONSTRAINT [FK_VoiceHistory_Users2] FOREIGN KEY ([Id_Receiver]) REFERENCES [dbo].[Users] ([Id_User])
);
