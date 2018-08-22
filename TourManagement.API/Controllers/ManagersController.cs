using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TourManagement.API.Services;
using TourManagement.API.Entities;
using TourManagement.API.Dtos;

namespace TourManagement.API.Controllers
{
    [Route("api/managers")]
    [Authorize]
    public class ManagersController : BaseController
    {
        private readonly ITourManagementRepository _tourManagementRepository;

        public ManagersController(ITourManagementRepository tourManagementRepository)
        {
            _tourManagementRepository = tourManagementRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetManagers()
        {
            var managersFromRepo = await _tourManagementRepository.GetManagers();
            var managers = Mapper.Map<IEnumerable<Dtos.Manager>>(managersFromRepo);
            return Ok(managers);    
        }
    }
}