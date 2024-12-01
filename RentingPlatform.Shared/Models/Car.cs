using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RentingPlatform.Shared
{

    
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CarId { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [MaxLength(10)]
        public string PlateNumber { get; set; }

        public decimal AverageRating { get; set; }

        public virtual List<Ratings> Ratings { get; set; } = new List<Ratings>();

        [Range(1, 5, ErrorMessage = "The rating must be between 1 and 5.")]
        public int? Rating { get; set; }

        [Required]
        public string Location { get; set; }

        public string Description { get; set; }
        
        public byte[]? Image { get; set; }

        public Guid? OwnerId { get; set; }

        public virtual Users? Owner { get; set; }

                public void AddRating(int rating, Guid userId)
        {
            if (userId == OwnerId)
                throw new UnauthorizedAccessException("Owners cannot rate their own Airbnb.");

            AverageRating = (AverageRating * Ratings.Count + rating) / (Ratings.Count + 1);
            Ratings.Add(new Ratings { UserId = userId, RatingValue = rating });
        }
    }
}