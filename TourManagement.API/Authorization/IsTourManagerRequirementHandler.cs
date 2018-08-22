using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using TourManagement.API.Services;
using System.Threading.Tasks;

namespace TourManagement.API.Authorization
{
    public class IsTourManagerRequirementHandler : AuthorizationHandler<RoleAuthorizationRequirement>
    {
        private IUserInfoService _userInfoService;
        private ITourManagementRepository _tourManagementRepository;

        public IsTourManagerRequirementHandler(IUserInfoService userInfoService, ITourManagementRepository tourManagementRepository)
        {
            _userInfoService = userInfoService;
            _tourManagementRepository = tourManagementRepository;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleAuthorizationRequirement requirement)
        {
            if (_userInfoService.Role == requirement.Role)
            {
                context.Succeed(requirement);
                return Task.FromResult(0);
            }

            if (Guid.TryParse(context.Resource.ToString(), out Guid tourManagerId))
            {
                if (Guid.TryParse(_userInfoService.UserId, out Guid userId))
                {
                    if (tourManagerId == userId)
                    {
                        context.Succeed(requirement);
                        return Task.FromResult(0);
                    }
                }

                context.Fail();
                return Task.FromResult(0);
            }
            
            var requestContext = context.Resource as AuthorizationFilterContext;

            if (requestContext == null) 
            {
                context.Fail();
                return  Task.FromResult(0);
            }

            var tourParam = requestContext.RouteData.Values["tourId"] as string;

            if (tourParam == null)
            {
                context.Fail();
                return Task.FromResult(0);
            }

            if (!Guid.TryParse(tourParam, out Guid tourId))
            {
                context.Fail();
                return Task.FromResult(0);
            }

/*             if (!Guid.TryParse(_userInfoService.UserId, out userId))
            {
                context.Fail();
                return Task.FromResult(0);
            } */

            bool isCurrentUserTourManager = _tourManagementRepository.IsTourManager(tourId, Guid.Parse(_userInfoService.UserId)).Result;

            if (!isCurrentUserTourManager)
            {
                context.Fail();
                return Task.FromResult(0);
            }
            
            context.Succeed(requirement);
            return Task.FromResult(0);
        }
    }
}