using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourManagement.API.Entities
{
    public class Manager : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ManagerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}