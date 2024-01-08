using ProfileServiceApp.Models;

namespace ProfileServiceApp.Interfaces
{
    public interface UserInterface
    {
        ICollection<User> GetUsers(); // Retrieve a collection of users
        User GetUser(int userId); // Retrieve a user by their identifier
        ICollection<User> GetUserOfAProfile(int profileId); // Retrieve users of a profile
        ICollection<Profile> GetProfileByUser(int userId); // Retrieve profiles associated with a user
        bool UserExists(int userId); // Check if a user with a given identifier exists
        bool CreateUser(User user); // Create a new user
        bool UpdateUser(User user); // Update an existing user
        bool DeleteUser(User user); // Delete a user
        bool Save(); // Save changes
    }
}
