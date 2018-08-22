using System;
using System.Collections;
using System.Collections.Generic;

namespace TourManagement.API.Dtos
{
    public class TourWithEstimatedProfitsAndManagerAndShowsForUpdate : TourWithEstimatedProfitsAndManagerForUpdate 
    {
        public ICollection<ShowForUpdate> Shows = new List<ShowForUpdate>();    
    }
}