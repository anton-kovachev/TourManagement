using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourManagement.API.Entities
{
    [Table("Shows")]
    public class Show : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ShowId { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; }

        [Required]
        [MaxLength(150)]
        public string Venue { get; set; }

        [Required]
        [MaxLength(150)]
        public string City { get; set; }

        [Required]
        [MaxLength(150)]
        public string Country { get; set; }

        public Guid TourId { get; set; }

        [ForeignKey("TourId")]
        public Tour Tour { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}