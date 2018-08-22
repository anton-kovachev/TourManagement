using System.Collections.Generic;

namespace TourManagement.API.Dtos
{
    public class TourWithShows : Tour
    {
        public IEnumerable<Show> Shows = new List<Show>();
    }
}