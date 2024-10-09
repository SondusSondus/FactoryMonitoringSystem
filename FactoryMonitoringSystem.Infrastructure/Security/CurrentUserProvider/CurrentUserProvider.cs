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
                var expiryTime = DateTime.Parse(GetSingleClaimValue("expiryRefreshTokenTime"));
                var role = GetClaimValues(ClaimTypes.Role).First();
                var userName = GetSingleClaimValue(ClaimTypes.Name);
                var email = GetSingleClaimValue(ClaimTypes.Email);
                return new CurrentUser(id, userName, email, string.Empty, expiryTime, role);
              
            }
            return new CurrentUser(new Guid("00000000-0000-0000-0000-000000000000"), "System", null, null, DateTime.MinValue, null);


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
