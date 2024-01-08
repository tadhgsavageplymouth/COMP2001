CREATE TABLE [dbo].[CW2_Following] (
    [UserID]            INT NOT NULL,
    [NumberofFollowing] INT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[CW2_Users] ([UserID])
);

