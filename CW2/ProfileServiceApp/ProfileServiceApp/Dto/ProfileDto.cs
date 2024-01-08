using ProfileServiceApp.Models;

namespace ProfileServiceApp.Dto
{
    public class ProfileDto
    {
        public int Id { get; set; } // Identifier for the profile DTO
        public string Title { get; set; } // Title of the profile DTO
        public string Text { get; set; } // Text content of the profile DTO
    }
}
