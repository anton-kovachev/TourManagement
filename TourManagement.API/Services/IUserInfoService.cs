using System.Security.Claims;

namespace TourManagement.API.Services
{
    public interface IUserInfoService
    {
        string UserId { get; }

        string FirstName { get; }

        string LastName { get; }

        string Role { get; }
    }
}