CREATE TABLE [dbo].[CW2_Followers] (
    [UserID]            INT NOT NULL,
    [NumberofFollowers] INT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[CW2_Users] ([UserID])
);

