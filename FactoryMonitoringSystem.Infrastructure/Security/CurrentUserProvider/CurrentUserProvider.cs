using FactoryMonitoringSystem.Shared;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FactoryMonitoringSystem.Infrastructure.Security.CurrentUserProvider
{
    public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider , IScopedDependency
    {
        public CurrentUser GetCurrentUser()
        {

            if(_httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true)
            {
                var id = Guid.Parse(GetSingleClaimValue("id"));
                var role = GetClaimValues(ClaimTypes.Role).First();
                var userName = GetSingleClaimValue(ClaimTypes.Name);
                var email = GetSingleClaimValue(ClaimTypes.Email);
                return new CurrentUser(id, userName, email, role);
              
            }
            return new CurrentUser(new Guid("299a20c5-9d80-4b39-9b6c-98cbcfffe9e1"), "Admain", string.Empty, string.Empty);


        }

        private List<string> GetClaimValues(string claimType) =>
            _httpContextAccessor.HttpContext!.User.Claims
                .Where(claim => claim.Type == claimType)
                .Select(claim => claim.Value)
                .ToList();

        private string GetSingleClaimValue(string claimType) =>
            _httpContextAccessor.HttpContext!.User.Claims
                .Single(claim => claim.Type == claimType)
                .Value;
    }
}
