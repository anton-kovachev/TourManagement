using System;
using System.Collections.Generic;

namespace TourManagement.API.Dtos
{
    public class TourWithEstimatedProfitsAndManagerAndShows : TourWithEstimatedProfitsAndManager
    {
        public ICollection<Show> Shows = new List<Show>();
    }
}