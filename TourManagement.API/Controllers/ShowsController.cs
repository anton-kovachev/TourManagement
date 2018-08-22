using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TourManagement.API.Services;
using TourManagement.API.Dtos;

namespace TourManagement.API.Controllers
{
    [Route("api/tours/{tourId}/shows")]
    [Authorize]
    public class ShowsController : BaseController
    {
        private readonly ITourManagementRepository _tourManagementRepostitory;

        public ShowsController(ITourManagementRepository tourManagementRepostitory)
        {
            _tourManagementRepostitory = tourManagementRepostitory;
        }        

        [HttpGet]
        public async Task<IActionResult> GetShows(Guid tourId)
        {
            if (!(await _tourManagementRepostitory.TourExists(tourId)))
            {
                return NotFound();
            }

            var showsFromRepo = await _tourManagementRepostitory.GetShows(tourId);
            var shows = Mapper.Map<IEnumerable<Show>>(showsFromRepo);

            return Ok(shows);
        }
    }
}