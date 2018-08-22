using System;

namespace TourManagement.API.Dtos
{
    public class TourWithEstimatedProfitsAndManager : TourWithEstimatedProfits
    {
        public Guid? ManagerId { get; set; }
           
    }
}