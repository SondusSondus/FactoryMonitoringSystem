using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.VerifyEmail
{
    public record VerifyEmailCommand(VerifyEmailRequest VerifyEmailRequest) :IRequest<ErrorOr<Success>>;
    
}
