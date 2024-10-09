using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Queries.GetUsers
{
    internal class GetUsersQueryHandler(IUserService userService) : IRequestHandler<GetUsersQuery, ErrorOr<List<UserResponse>>>
    {
        private readonly IUserService _userService = userService;
        public async Task<ErrorOr<List<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        => await _userService.GetUsers(cancellationToken);

    }
}
