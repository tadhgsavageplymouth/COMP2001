CREATE TABLE [dbo].[CW2_Profile] (
    [UserID]      INT            NOT NULL,
    [Insights]    NVARCHAR (MAX) NULL,
    [ProfileFeed] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[CW2_Users] ([UserID])
);

