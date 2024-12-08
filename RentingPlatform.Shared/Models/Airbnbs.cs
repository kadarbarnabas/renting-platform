using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentingPlatform.Shared
{
    public class Airbnbs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AirbnbId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PricePerNight { get; set; }

        [Required]
        public string Location { get; set; }

        public string Country { get; set; } 

        [Required]
        public int MaxGuests { get; set; }

        public string Amenities { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual Users Owner { get; set; }

        public virtual List<string> ImageUrls { get; set; } = new List<string>();

        public decimal AverageRating { get; set; }

        public virtual List<Ratings> Ratings { get; set; } = new List<Ratings>();

        public virtual List<AirbnbBookings> Timetable { get; set; } = new List<AirbnbBookings>();

        public int Rooms { get; set; } 
        public int Beds { get; set; } 
        public int Bathrooms { get; set; } 

        public DateTime? GetNextAvailableDate()
        {
            var today = DateTime.Today;
            foreach (var booking in Timetable)
            {
                if (booking.EndDate > today)
                    today = booking.EndDate;
            }
            return today;
        }

        public decimal CalculateTotalPrice(DateTime startDate, DateTime endDate)
        {
            var days = (endDate - startDate).Days;
            return days * PricePerNight;
        }

        public void AddImage(string imageUrl, Guid userId)
        {
            if (userId != OwnerId)
                throw new UnauthorizedAccessException("Only the owner can upload images.");
            ImageUrls.Add(imageUrl);
        }

        public void AddRating(int rating, Guid userId)
        {
            if (userId == OwnerId)
                throw new UnauthorizedAccessException("Owners cannot rate their own Airbnb.");

            AverageRating = (AverageRating * Ratings.Count + rating) / (Ratings.Count + 1);
            Ratings.Add(new Ratings { UserId = userId, RatingValue = rating });
        }
    }
public class Ratings
{
    [Key]
    public Guid RatingId { get; set; }  // Add RatingId as a primary key

    public Guid UserId { get; set; }
    public int RatingValue { get; set; }

    public Guid AirbnbId { get; set; } 
}


    public class AirbnbBookings
    {
        [Key]
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid AirbnbId { get; set; }

        /*[ForeignKey(nameof(AirbnbId))]
        public virtual Airbnbs Airbnb { get; set; }*/
        
    }
}
