using Microsoft.AspNetCore.Mvc;

namespace TourManagement.API.Helpers
{
    public class InternalServerErrorResult : StatusCodeResult
    {
        public InternalServerErrorResult() : base(500)
        {
        }
    }
}