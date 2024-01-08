using ProfileServiceApp.Models;

namespace ProfileServiceApp.Interfaces
{
    public interface UserStatInterface
    {
        ICollection<UserStat> GetUserStats(); // Retrieve a collection of user stats
        UserStat GetUserStat(int userStatId); // Retrieve a user stat by its identifier
        ICollection<UserStat> GetUserStatsOfAProfile(int profileId); // Retrieve user stats of a profile
        bool UserStatExists(int userStatId); // Check if a user stat with a given identifier exists
        bool CreateUserStat(UserStat userStat); // Create a new user stat
        bool UpdateUserStat(UserStat userStat); // Update an existing user stat
        bool DeleteUserStat(UserStat userStat); // Delete a user stat
        bool DeleteUserStats(List<UserStat> userStats); // Delete a list of user stats
        bool Save(); // Save changes
    }
}
