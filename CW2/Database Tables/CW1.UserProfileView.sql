CREATE VIEW CW1.UserProfileView AS
SELECT U.UserID,
       U.Username,
       U.ProfilePhoto,
       U.Location,
       U.MembershipStartDate,
       U.NumberofFollowers,
       U.NumberofFollowing,
       P.Statistics,
       P.ProfileFeed
FROM CW1_Ex3_Ex2_1NF_Users U
JOIN CW1_Ex3_Ex2_1NF_Profile P ON U.UserID = P.UserID;
