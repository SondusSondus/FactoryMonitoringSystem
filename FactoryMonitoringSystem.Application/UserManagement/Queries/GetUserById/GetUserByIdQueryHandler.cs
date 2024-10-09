using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Queries.GetUserById
{
    internal class GetUserByIdQueryHandler(IUserService userService) : IRequestHandler<GetUserByIdQuery, ErrorOr<UserResponse>>
    {
        private readonly IUserService _userService = userService;
        public async Task<ErrorOr<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        => await _userService.GetUserById(request.Id, cancellationToken);
    }
}
