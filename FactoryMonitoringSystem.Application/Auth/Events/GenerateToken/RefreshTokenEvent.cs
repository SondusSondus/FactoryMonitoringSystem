using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Auth.Events.GenerateToken
{
    public record RefreshTokenEvent(string refreshToken) :INotification;
   
}
