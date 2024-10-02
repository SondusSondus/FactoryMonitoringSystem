using FactoryMonitoringSystem.Shared;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FactoryMonitoringSystem.Infrastructure.Security.CurrentUserProvider
{
    public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider , IScopedDependency
    {
        public CurrentUser GetCurrentUser()
        {
           // _httpContextAccessor.HttpContext.ThrowIfNull();

            var id = Guid.Parse(GetSingleClaimValue("id"));
            var expiryTime = DateTime.Parse(GetSingleClaimValue("expiryTime"));
            var roles = GetClaimValues(ClaimTypes.Role);
            var userName = GetSingleClaimValue(ClaimTypes.Name);
            var email = GetSingleClaimValue(ClaimTypes.Email);

            return new CurrentUser(id, userName, email, string.Empty ,expiryTime ,roles);
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
