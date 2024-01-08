CREATE TABLE [dbo].[CW2_UserStats] (
    [UserID]              INT  NOT NULL,
    [MembershipStartDate] DATE NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[CW2_Users] ([UserID])
);

