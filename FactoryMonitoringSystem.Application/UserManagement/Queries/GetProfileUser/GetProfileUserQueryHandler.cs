using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Queries.GetProfileUser
{
    internal class GetProfileUserQueryHandler(IUserService userService) : IRequestHandler<GetProfileUserQuery, ErrorOr<UserResponse>>
    {
        private readonly IUserService _userService = userService;
        public async Task<ErrorOr<UserResponse>> Handle(GetProfileUserQuery request, CancellationToken cancellationToken)
            => await _userService.GetUserAsync(cancellationToken);

    }
}
