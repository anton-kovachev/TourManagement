using System;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using IdentityModel;


namespace TourManagement.API.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string UserId { get { return _httpContextAccessor?.HttpContext?.User.Claims
                                                .Where( c => c.Type == JwtClaimTypes.Subject).FirstOrDefault()?.Value ?? "n/a"; } }
        public string FirstName { get { return _httpContextAccessor?.HttpContext?.User.Claims
                                                .Where( c => c.Type == JwtClaimTypes.GivenName).FirstOrDefault()?.Value ?? "n/a"; } }
        public string LastName { get { return _httpContextAccessor?.HttpContext?.User.Claims
                                                .Where( c => c.Type == JwtClaimTypes.FamilyName).FirstOrDefault()?.Value ?? "n/a"; } }

        public string Role { get { return _httpContextAccessor?.HttpContext?.User.Claims
                                                .Where( c => c.Type == JwtClaimTypes.Role).FirstOrDefault()?.Value ?? "n/a"; } }
        public UserInfoService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentException(nameof(httpContextAccessor));
/*             var currentContext = _httpContextAccessor.HttpContext;

            if (currentContext == null || !currentContext.User.Identity.IsAuthenticated)
            {
                UserId = "n/a";
                FirstName = "n/a";
                LastName = "n/a";
                Role = "n/a";
            }
            else
            {
                IEnumerable<Claim> userClaims = httpContextAccessor.HttpContext.User.Claims;

                //UserId = userClaims.Where( c => c.Type == JwtClaimTypes.Subject).FirstOrDefault()?.Value ?? "n/a";
                FirstName = userClaims.Where( c => c.Type == JwtClaimTypes.GivenName).FirstOrDefault()?.Value ?? "n/a";
                LastName = userClaims.Where( c => c.Type == JwtClaimTypes.FamilyName).FirstOrDefault()?.Value ?? "n/a";
                //Role = userClaims.Where( c => c.Type == JwtClaimTypes.Role).FirstOrDefault()?.Value ?? "n/a";
            } */
        }
    }
}