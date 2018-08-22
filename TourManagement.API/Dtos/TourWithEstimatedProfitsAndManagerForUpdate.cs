using System;
using System.ComponentModel.DataAnnotations;

namespace TourManagement.API.Dtos
{
    public class TourWithEstimatedProfitsAndManagerForUpdate : TourWithEstimatedProfitsForUpdate
    {
        public Guid? ManagerId { get; set; }
    }
}