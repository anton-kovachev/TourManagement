using System;
using System.Collections.Generic;

namespace TourManagement.API.Dtos
{
    public class TourWithShowsForUpdate : TourForUpdate
    {
        public ICollection<ShowForUpdate> shows = new List<ShowForUpdate>();   
    }
}