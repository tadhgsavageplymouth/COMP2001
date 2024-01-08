CREATE TABLE [dbo].[CW2_Trails] (
    [TrailID]         INT            NOT NULL,
    [UserID]          INT            NULL,
    [PhotosOfTrails]  NVARCHAR (MAX) NULL,
    [ReviewsOfTrails] NVARCHAR (MAX) NULL,
    [Activities]      NVARCHAR (MAX) NULL,
    [CompletedTrails] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([TrailID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[CW2_Users] ([UserID])
);

