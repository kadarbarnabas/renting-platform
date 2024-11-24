using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentingPlatform.Shared
{
    public class Airbnb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AirbnbId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; } 

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PricePerNight { get; set; } 

        [Required]
        public string Location { get; set; } 

        [Required]
        public int MaxGuests { get; set; } 

        public string Amenities { get; set; } 

        [Required]
        public Guid OwnerId { get; set; } 

        [ForeignKey(nameof(OwnerId))]
        public Users Owner { get; set; } 

        public string ImageUrl { get; set; } 

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
