using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Responses;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Queries.GetUsers
{
    public record GetUsersQuery : IRequest<ErrorOr<List<UserResponse>>>;
    
}
