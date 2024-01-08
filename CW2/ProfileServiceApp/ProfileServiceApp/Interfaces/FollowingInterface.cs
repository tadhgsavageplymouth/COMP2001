using ProfileServiceApp.Models;

namespace ProfileServiceApp.Interfaces
{
    public interface FollowingInterface
    {
        ICollection<Following> GetFollowings(); // Retrieve a collection of followings
        Following GetFollowing(int id); // Retrieve a following by its identifier
        Following GetFollowingByUser(int UserId); // Retrieve a following by user ID
        ICollection<User> GetUserFromAFollowing(int followingId); // Retrieve users from a following
        bool FollowingExists(int id); // Check if a following with a given identifier exists
        bool CreateFollowing(Following following); // Create a new following
        bool UpdateFollowing(Following following); // Update an existing following
        bool DeleteFollowing(Following following); // Delete a following
        bool Save(); // Save changes
    }
}
