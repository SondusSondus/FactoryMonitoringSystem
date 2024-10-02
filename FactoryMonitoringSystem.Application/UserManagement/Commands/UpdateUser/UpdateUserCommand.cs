using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.UpdateUser
{
    public record UpdateUserCommand(UpdateUserRequest UpdateUser) : IRequest<ErrorOr<Success>>;

}
