CREATE TABLE [dbo].[CW2_Users] (
    [UserID]       INT            NOT NULL,
    [Username]     NVARCHAR (50)  NULL,
    [ProfilePhoto] NVARCHAR (255) NULL,
    [Location]     NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

