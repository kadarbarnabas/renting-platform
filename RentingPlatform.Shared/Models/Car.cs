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

        public string Description { get; set; }
        
        [Required]
        public byte[] Image { get; set; }
    }
}