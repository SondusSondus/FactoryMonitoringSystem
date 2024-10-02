using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Responses;
using MediatR;


namespace FactoryMonitoringSystem.Application.UserManagement.Queries.GetProfileUser
{
    public record GetProfileUserQuery(): IRequest<ErrorOr<UserResponse>>;
    
}
