-- User Table
CREATE TABLE CW1.Users (
    UserID INT PRIMARY KEY,
    Username NVARCHAR(50),
    Email NVARCHAR(100),
    Password NVARCHAR(100)
);

-- Sample Data for Users
INSERT INTO CW1.Users (UserID, Username, Email, Password)
VALUES 
(1, 'Grace Hopper', 'grace@plymouth.ac.uk', 'ISAD123!'),
(2, 'Tim Berners-Lee', 'tim@plymouth.ac.uk', 'COMP2001!'),
(3, 'Ada Lovelace', 'ada@plymouth.ac.uk', 'insecurePassword');

-- Profile Table
CREATE TABLE CW1.Profile (
    ProfileID INT PRIMARY KEY,
    UserID INT UNIQUE,
    ProfileName NVARCHAR(50),
    Bio NVARCHAR(MAX),
    FOREIGN KEY (UserID) REFERENCES CW1.Users(UserID)
);

-- Sample Data for Profile
INSERT INTO CW1.Profile (ProfileID, UserID, ProfileName, Bio)
VALUES 
(1, 1, 'Grace Profile', 'Bio for Grace'),
(2, 2, 'Tim Profile', 'Bio for Tim'),
(3, 3, 'Ada Profile', 'Bio for Ada');

-- Photo Table
CREATE TABLE CW1.Photo (
    PhotoID INT PRIMARY KEY,
    ProfileID INT,
    PhotoURL NVARCHAR(MAX),
    FOREIGN KEY (ProfileID) REFERENCES CW1.Profile(ProfileID)
);

-- Sample Data for Photo
INSERT INTO CW1.Photo (PhotoID, ProfileID, PhotoURL)
VALUES 
(1, 1, 'photo_url_1.jpg'),
(2, 1, 'photo_url_2.jpg'),
(3, 2, 'photo_url_3.jpg');
