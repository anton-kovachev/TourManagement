using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using IdentityModel;

using TourManagementIdentityServer.Data;
using TourManagementIdentityServer.Models;

namespace TourManagementIdentityServer.Helpers
{
    public static class ApplicationDbContextExtensions
    {
        public static async Task EnsureSeedDatabaseAsync(this ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            string bobEmail = "bob@gmail.com";
            var bobUser = new ApplicationUser { Id = "57525bb1-395d-4958-8711-25e78cf634a1", UserName = bobEmail, Email = bobEmail };

            await userManager.CreateAsync(bobUser, "Bob@123");

            string aliceEmail = "alice@gmail.com";
            var aliceUser = new ApplicationUser { Id = "57525bb1-395d-4958-8711-25e78cf634a0", UserName = aliceEmail, Email = aliceEmail };

            await userManager.CreateAsync(aliceUser, "Alice@123");

            string frnakEmail = "frank@gmail.com";
            var frankUser = new ApplicationUser { Id = "cc3012a4-74f6-48f8-8199-2e2f7096ec75", UserName = frnakEmail, Email = frnakEmail };

            await userManager.CreateAsync(frankUser, "Frank@123");

            await userManager.AddClaimAsync(frankUser, new Claim(JwtClaimTypes.Role, "Admin"));
        }   
    }
}