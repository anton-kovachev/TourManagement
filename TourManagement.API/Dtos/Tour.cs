using System;

namespace TourManagement.API.Dtos
{
    public class Tour : TourAbstractBase
    {
        public Guid TourId { get; set; }
        public string Band  { get; set; }

        public bool IsUserTourManager { get; set; }
    }
}