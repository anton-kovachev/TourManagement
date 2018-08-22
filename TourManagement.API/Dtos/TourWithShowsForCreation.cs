using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourManagement.API.Dtos
{
    public class TourWithShowsForCreation : TourForCreation 
    {
        public ICollection<ShowForCreation> Shows { get; set; } = new List<ShowForCreation>();
    }
}