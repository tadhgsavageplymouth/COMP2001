using ProfileServiceApp.Dto;
using ProfileServiceApp.Models;

namespace ProfileServiceApp.Interfaces
{
    public interface ProfileInterface
    {
        ICollection<Profile> GetProfiles(); // Retrieve a collection of profiles
        Profile GetProfile(int id); // Retrieve a profile by its identifier
        Profile GetProfile(string name); // Retrieve a profile by name
        Profile GetProfileTrimToUpper(ProfileDto profileCreate); // Retrieve a profile by trimmed and uppercased name
        decimal GetProfileRating(int profileId); // Retrieve the rating of a profile by its identifier
        bool ProfileExists(int profileId); // Check if a profile with a given identifier exists
        bool CreateProfile(int userId, int followerId, Profile profile); // Create a new profile with owner and category IDs
        bool UpdateProfile(int userId, int followerId, Profile profile); // Update an existing profile with owner and category IDs
        bool DeleteProfile(Profile profile); // Delete a profile
        bool Save(); // Save changes
    }
}
