using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Auth.Commands.InvalidateRefreshToken
{
    public record InvalidateRefreshTokenCommand():IRequest<ErrorOr<Success>>;
  
}
