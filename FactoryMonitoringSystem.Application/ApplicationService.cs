using FactoryMonitoringSystem.Domain.Common.Repositories;
using FactoryMonitoringSystem.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace FactoryMonitoringSystem.Application
{
    public class ApplicationService<T,TEntity> where T : class  where TEntity : class 
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected T GetService<T>()
        {
            var resolver = _httpContextAccessor.HttpContext?.RequestServices;

            return resolver.GetService<T>();
        }

        protected IMediator Mediator => GetService<IMediator>();
        protected ILogger<T> Logger => GetService<ILogger<T>>();
        protected IWriteRepository<TEntity> WriteRepository => GetService<IWriteRepository<TEntity>>();
        protected IReadRepository<TEntity> ReadRepository => GetService<IReadRepository<TEntity>>();
        protected Guid GuidGenerator => Guid.NewGuid();
        protected CurrentUser CurrentUser => GetService<CurrentUser>();
        protected Guid LoggedInUserId => CurrentUser.Id;


    }
}
