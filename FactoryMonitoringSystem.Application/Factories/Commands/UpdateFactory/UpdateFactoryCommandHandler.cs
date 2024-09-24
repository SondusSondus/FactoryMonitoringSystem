using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Application.Factories.Commands.UpdateFactor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Factories.Commands.UpdateFactory
{
    public class UpdateFactoryCommandHandler : IRequestHandler<UpdateFactoryCommand, ErrorOr<FactoryResponse>>
    {
        public Task<ErrorOr<FactoryResponse>> Handle(UpdateFactoryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
