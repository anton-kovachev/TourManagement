using System.Collections.Generic;

namespace TourManagement.API.Dtos
{
    public class TourWithEstimatedProfitsAndShows: TourWithEstimatedProfits
    {
        public IEnumerable<Show> Shows = new List<Show>();
    }
}