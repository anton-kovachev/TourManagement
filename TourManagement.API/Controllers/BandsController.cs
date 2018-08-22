using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourManagement.API.Dtos;
using TourManagement.API.Services;

namespace TourManagement.API.Controllers
{
    [Authorize]
    [Route("api/bands")]
    public class BandsController : BaseController 
    {
        private ITourManagementRepository _tourManagementRepository;

        public BandsController(ITourManagementRepository tourManagementRepository)
        {
            _tourManagementRepository = tourManagementRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBands()
        {
            var bandFromRepo = await _tourManagementRepository.GetBands();
            var bands = AutoMapper.Mapper.Map<IEnumerable<Band>>(bandFromRepo);

            return Ok(bands);
        }
    }
}