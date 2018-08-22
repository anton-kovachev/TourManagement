using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourManagement.API.Entities;

namespace TourManagement.API.Services
{
    public class TourManagementContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Tour> Tours { get; set; }

        private readonly IUserInfoService _userInfoService;

        public TourManagementContext(DbContextOptions<TourManagementContext> options, IUserInfoService userInfoService)
        : base(options)
        {
            _userInfoService = userInfoService ?? throw new ArgumentNullException(nameof(userInfoService));
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var addedEntities = ChangeTracker.Entries().Where( e => e.State == EntityState.Added ).ToList();

            addedEntities.ForEach( e => { 

                var addedAuditableEntry = e.Entity as AuditableEntity;

                if (e.State == EntityState.Added) 
                {
                    addedAuditableEntry.CreatedBy = _userInfoService.UserId;
                    addedAuditableEntry.CreatedOn = DateTime.UtcNow;
                }

                addedAuditableEntry.UpadatedOn = DateTime.UtcNow;
                addedAuditableEntry.UpdatedBy = _userInfoService.UserId;
            });

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}