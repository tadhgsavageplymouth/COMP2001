using ProfileServiceApp.Models;

namespace ProfileServiceApp.Interfaces
{
    public interface UserStatInterface
    {
        ICollection<UserStat> GetUserStats(); // Retrieve a collection of user stats
        UserStat GetUserStat(int userStatId); // Retrieve a user stat by its identifier
        ICollection<Trail> GetTrailsByUserStat(int userStatId); // Retrieve trails associated with a user stat
        bool UserStatExists(int userStatId); // Check if a user stat with a given identifier exists
        bool CreateUserStat(UserStat userStat); // Create a new user stat
        bool UpdateUserStat(UserStat userStat); // Update an existing user stat
        bool DeleteUserStat(UserStat userStat); // Delete a user stat
        bool Save(); // Save changes
    }
}
