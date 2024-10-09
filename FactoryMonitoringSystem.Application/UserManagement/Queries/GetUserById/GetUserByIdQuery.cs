using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Responses;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<ErrorOr<UserResponse>>;

}
