using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourManagement.API.Entities;
using TourManagement.API.Services;

namespace TourManagement.API.Services
{
    public class TourManagementRepository : ITourManagementRepository
    {
        private TourManagementContext _context;

        public TourManagementRepository(TourManagementContext context)
        {
            _context = context;
        }

         public async Task AddTour(Tour tour)
         {
             await _context.Tours.AddAsync(tour);
         }

         #pragma warning disable 1998
         public async Task DeleteTour(Tour tour)
         {
             _context.Tours.Remove(tour);
         }

         public async Task<Tour> GetTour(Guid tourId, bool includeShows = false)
         {
             if (includeShows)
             {
                 return await _context.Tours.Include(t => t.Shows).Include(t => t.Band)
                    .Where( t => t.TourId == tourId).FirstOrDefaultAsync();
             }
             
             return await _context.Tours.Include(t => t.Band).Where( t => t.TourId == tourId).FirstOrDefaultAsync();
         }

         public async Task<IEnumerable<Tour>> GetTours(bool includeShows = false)
         {
             if (includeShows)
             {
                 return await _context.Tours.Include(t => t.Shows).Include(t => t.Band).ToListAsync();
             }

             return await _context.Tours.Include(t => t.Band).ToListAsync();
         }

         public async Task<bool> IsTourManager(Guid tourId, Guid managerId)
         {
             return await _context.Tours.AnyAsync(t => t.TourId == tourId && t.ManagerId == managerId);
         }

         public async Task<bool> SaveAsync()
         {
             int result = await _context.SaveChangesAsync();
             return result >= 0;
         }

         public async Task<bool> TourExists(Guid tourId)
         {
             return await _context.Tours.AnyAsync(t => t.TourId == tourId);
         }

        #pragma warning disable 1998
         public async Task UpdateTour(Tour tour)
         {
             _context.Attach(tour).State = EntityState.Modified;
         }

         public async Task<IEnumerable<Show>> GetShows(Guid tourId)
         {
             return await _context.Shows.Where(s => s.TourId == tourId).ToListAsync();
         }

         public async Task<IEnumerable<Show>> GetShows(Guid tourId, IEnumerable<Guid> showIds)
         {
             return await _context.Shows.Where(s => s.TourId == tourId &&  showIds.Contains(s.ShowId))
                .ToListAsync();
         }

         public async Task AddShow(Guid tourId, Show show)
         {
            Tour tour = await this.GetTour(tourId); 
            
            if (tour == null) 
            {
                throw new Exception($"Cannot fined tour with id: {tourId}");
            }

            tour.Shows.Add(show);
         }

         public async Task<IEnumerable<Band>> GetBands() => await Task.FromResult(this._context.Bands.AsEnumerable());
         
         public async Task<Band> GetBand(Guid bandId) => await Task.FromResult(this._context.Bands.Where(b => b.BandId == bandId).SingleOrDefault());

         public async Task<IEnumerable<Manager>> GetManagers() => await _context.Managers.ToListAsync();
    }
}