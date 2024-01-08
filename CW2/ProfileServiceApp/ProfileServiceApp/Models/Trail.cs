namespace ProfileServiceApp.Models
{
    public class Trail
    {
        public int TrailID { get; set; } // Identifier for the trail
        public int? UserID { get; set; } // User ID associated with the trail
        public string PhotosOfTrails { get; set; } // Photos related to the trails
        public string ReviewsOfTrails { get; set; } // Reviews for the trails
        public string Activities { get; set; } // Activities associated with the trail
        public string CompletedTrails { get; set; } // List of completed trails

        public User User { get; set; } // User associated with this trail

        public ICollection<Trail> Trails { get; set; } // Collection of trails
    }
}
