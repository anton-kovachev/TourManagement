using System;
using Microsoft.AspNetCore.Mvc;

using TourManagement.API.Helpers;

namespace TourManagement.API.Controllers
{
    public class BaseController : Controller
    {
        public InternalServerErrorResult InternalServerError() => new InternalServerErrorResult();
    }
}