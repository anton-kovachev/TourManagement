using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using TourManagement.API.Services;
using TourManagement.API.Dtos;
using TourManagement.API.Helpers;
using TourManagement.API.Authorization;

namespace TourManagement.API.Controllers
{
    [Route("api/tours")]
    [Authorize]
    public class ToursController : BaseController
    {
        private readonly ITourManagementRepository _tourManagementRepository;
        private readonly IUserInfoService _userInfoService;

        public ToursController(ITourManagementRepository tourManagementRepository, IUserInfoService userInfoService)
        {
            _tourManagementRepository = tourManagementRepository;
            _userInfoService = userInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTours()
        {
            var toursFromRepo = (await _tourManagementRepository.GetTours());

            var tours = Mapper.Map<IEnumerable<Tour>>(toursFromRepo);

            return Ok(tours);
        }

        [HttpGet("{tourId}", Name = "GetTour")]
        public async Task<IActionResult> GetTour(Guid tourId)
        {
            var tourFromRepo = await _tourManagementRepository.GetTour(tourId);

            if (tourFromRepo == null)
            {
                return BadRequest();
            }

            var tour = Mapper.Map<Tour>(tourFromRepo);

            return Ok(tour);
        }

        [HttpGet("{tourId}", Name = "GetTour")]
        [RequestHeaderMatchesMediaType("Accept", new string[] { "application/vnd.toursltd.tourwithestimatedprofits+json" })]
        [Authorize(Policy="IsAdmin")]
        public async Task<IActionResult> GetTourWithEstimatedProfits(Guid tourId)
        {
            return await GetSpecificTour<TourWithEstimatedProfits>(tourId);
        }

        [HttpGet("{tourId}", Name = "GetTour")]
        [RequestHeaderMatchesMediaType("Accept", new string[] { "application/vnd.toursltd.tourwithestimatedprofitsandmanager+json" })]
        [Authorize(Policy="IsAdmin")]
        public async Task<IActionResult> GetTourWithEstimatedProfitsAndManager(Guid tourId)
        {
            return await GetSpecificTour<TourWithEstimatedProfitsAndManager>(tourId);
        }

        [HttpGet("{tourId}")]
        [RequestHeaderMatchesMediaType("Accept", new string[] { "application/vnd.toursltd.tourwithshows+json" })]
        [Authorize(Policy="IsTourManager")]
        public async Task<IActionResult> GetTourWithShows(Guid tourId)
        {
            return await GetSpecificTour<TourWithShows>(tourId, includeShows: true);
        }

        [HttpGet("{tourId}")]
        [RequestHeaderMatchesMediaType("Accept", new string[] { "application/vnd.toursltd.tourwithestimatedprofitsandshows+json" })]
        [Authorize(Policy="IsAdmin")]
        public async Task<IActionResult> GetTourWithEstimatedProfitsAndShows(Guid tourId)
        {
            return await GetSpecificTour<TourWithEstimatedProfitsAndShows>(tourId, includeShows: true);
        }

        [HttpGet("{tourId}")]
        [RequestHeaderMatchesMediaType("Accept", new string[] { "application/vnd.toursltd.tourwithestimatedprofitsandmanagerandshows+json" })]
        [Authorize(Policy="IsAdmin")]
        public async Task<IActionResult> GetTourWithEstimatedProfitsAndManagerAndShows(Guid tourId)
        {
            return await GetSpecificTour<TourWithEstimatedProfitsAndManagerAndShows>(tourId, includeShows: true);
        }

        private async Task<IActionResult> GetSpecificTour<T> (Guid tourId, bool includeShows = false) where T : class
        {
            var tourFromRepo = await this._tourManagementRepository.GetTour(tourId, includeShows);

            if (tourFromRepo == null)
            {
                return BadRequest();
            }

            return Ok(Mapper.Map<T>(tourFromRepo));
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type", new string[] { "application/vnd.toursltd.tourforcreation+json" })]
        public async Task<IActionResult> AddTour([FromBody] TourForCreation tour)
        {
            return await AddSpecificTour<TourForCreation>(tour);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type", new string[] { "application/vnd.toursltd.tourwithmanagerforcreation+json" })]
        [Authorize(Policy="IsAdmin")]
        public async Task<IActionResult> AddTourWithManager([FromBody] TourWithManagerForCreation tour)
        {
            return await AddSpecificTour<TourWithManagerForCreation>(tour);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type", new string[] { "application/vnd.toursltd.tourwithshowsforcreation+json" })]
        public async Task<IActionResult> AddTourWithShows([FromBody] TourWithShowsForCreation tour)
        {
            return await AddSpecificTour<TourWithShowsForCreation>(tour);
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Content-Type", new string[] { "application/vnd.toursltd.tourwithmanagerandshowsforcreation+json" })]
        [Authorize(Policy="IsAdmin")]
        public async Task<IActionResult> AddTourWithManagerAndShows([FromBody] TourWithManagerAndShowsForCreation tour)
        {
            return await AddSpecificTour<TourWithManagerAndShowsForCreation>(tour);
        }

        private async Task<IActionResult> AddSpecificTour<T>(T tour)
        {
            if (tour == null) 
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(new CustomizedValidationResult(ModelState));
            }

            Entities.Tour tourEntity = null;

            try 
            {
                tourEntity = Mapper.Map<Entities.Tour>(tour);
            }
            catch(Exception ex)
            {
                var e = ex.ToString();
            }

            if (tourEntity.ManagerId == null || tourEntity.ManagerId == Guid.Empty)
            {
                if (!Guid.TryParse(_userInfoService.UserId, out Guid userId))
                {
                    return BadRequest();
                }

                tourEntity.ManagerId = userId;
            }

            await _tourManagementRepository.AddTour(tourEntity);
                
            if (await _tourManagementRepository.SaveAsync()) 
            {
                return CreatedAtRoute("GetTour", new { tourId = tourEntity.TourId }, Mapper.Map<Dtos.Tour>(tourEntity));
            }
            else 
            {
                throw new Exception("The creation of the tour failed");
            }
        }

        [HttpPatch("{tourId}")]
        [RequestHeaderMatchesMediaType("Content-Type", new string[] { "application/vnd.toursltd.tourforupdate-json-patch+json" })]
        [Authorize(Policy="IsTourManager")]
        public async Task<IActionResult> UpdateTour(Guid tourId, [FromBody] JsonPatchDocument<TourForUpdate> tourForUpdatePatch)
        {
           return await UpdateSpecificTour<TourForUpdate>(tourId, tourForUpdatePatch);
        }

        [HttpPatch("{tourId}")]
        [RequestHeaderMatchesMediaType("Content-Type", new string[] { "application/vnd.toursltd.tourwithshowsforupdate-json-patch+json" })]
        [Authorize(Policy="IsTourManager")]
        public async Task<IActionResult> UpdateTourWithShows(Guid tourId, [FromBody] JsonPatchDocument<TourWithShowsForUpdate> tourForUpdatePatch)
        {
           return await UpdateSpecificTour<TourWithShowsForUpdate>(tourId, tourForUpdatePatch, includeShows: true);
        }

        [HttpPatch("{tourId}")]
        [RequestHeaderMatchesMediaType("Content-Type", new string[] { "application/vnd.toursltd.tourwithestimatedprofitsforupdate-json-patch+json" })]
        [Authorize(Policy="IsAdmin")]
        public async Task<IActionResult> UpdateTourWithEstimatedProfits(Guid tourId, [FromBody] JsonPatchDocument<TourWithEstimatedProfitsForUpdate> tourForUpdatePatch)
        {
            return await UpdateSpecificTour<TourWithEstimatedProfitsForUpdate>(tourId, tourForUpdatePatch);
        }

        
        [HttpPatch("{tourId}")]
        [RequestHeaderMatchesMediaType("Content-Type", new string[] { "application/vnd.toursltd.tourwithestimatedprofitsandmanagerforupdate-json-patch+json" })]
        [Authorize(Policy="IsAdmin")]
        public async Task<IActionResult> UpdateTourWithEstimatedProfitsAndManager(Guid tourId, [FromBody] JsonPatchDocument<TourWithEstimatedProfitsAndManagerForUpdate> tourForUpdatePatch)
        {
            return await UpdateSpecificTour<TourWithEstimatedProfitsAndManagerForUpdate>(tourId, tourForUpdatePatch);
        }

        [HttpPatch("{tourId}")]
        [RequestHeaderMatchesMediaType("Content-Type", new string[] { "application/vnd.toursltd.tourwithestimatedprofitsandmanagerandshowsforupdate-json-patch+json" })]
        [Authorize(Policy="IsAdmin")]
        public async Task<IActionResult> UpdateTourWithEstimatedProfitsAndManagerAndShows(Guid tourId, [FromBody] JsonPatchDocument<TourWithEstimatedProfitsAndManagerAndShowsForUpdate> tourForUpdatePatch)
        {
            return await UpdateSpecificTour<TourWithEstimatedProfitsAndManagerAndShowsForUpdate>(tourId, tourForUpdatePatch, includeShows: true);
        }

        private async Task<IActionResult> UpdateSpecificTour<T> (Guid tourId, JsonPatchDocument<T> tourForUpdatePatch, bool includeShows = false) where T : class
        {
            if (tourForUpdatePatch == null)
            {
                return BadRequest();
            }

            var tourFromRepo = await _tourManagementRepository.GetTour(tourId, includeShows);

            if (tourFromRepo == null)
            {
                return BadRequest();
            }

            var tourForUpdate = Mapper.Map<T>(tourFromRepo);

            tourForUpdatePatch.ApplyTo(tourForUpdate, ModelState);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(new CustomizedValidationResult(ModelState));
            }

            if (!TryValidateModel(tourForUpdate))
            {
                return UnprocessableEntity(new CustomizedValidationResult(ModelState));
            }
            
            Mapper.Map(tourForUpdate, tourFromRepo);

            await _tourManagementRepository.UpdateTour(tourFromRepo);

            if (await _tourManagementRepository.SaveAsync())
            {
                return NoContent();
            }
            else 
            {
                return InternalServerError();
            }
        }
      
    }
}