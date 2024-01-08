using ProfileServiceApp.Models;

namespace ProfileServiceApp.Interfaces
{
    public interface FollowerInterface
    {
        ICollection<Follower> GetFollowers(); // Retrieve a collection of followers
        Follower GetFollower(int id); // Retrieve a follower by their identifier
        bool FollowerExists(int id); // Check if a follower with a given identifier exists
        bool CreateFollower(Follower follower); // Create a new follower
        bool UpdateFollower(Follower follower); // Update an existing follower
        bool DeleteFollower(Follower follower); // Delete a follower
        bool Save(); // Save changes
    }
}
