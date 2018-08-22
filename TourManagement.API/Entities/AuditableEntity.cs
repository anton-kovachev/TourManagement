using System;

namespace TourManagement.API.Entities
{
    public class AuditableEntity
    {
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpadatedOn { get; set; }

        public string UpdatedBy { get; set; }
    }
}