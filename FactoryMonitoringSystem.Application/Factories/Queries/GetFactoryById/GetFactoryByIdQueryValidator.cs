using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FactoryMonitoringSystem.Application.Factories.Queries.GetFactoryById
{
    internal class GetFactoryByIdQueryValidator :AbstractValidator<GetFactoryByIdQuery>
    {
        public GetFactoryByIdQueryValidator()
        {
                RuleFor(factory=>factory.FactoryId)
                      .NotEmpty().WithMessage("Factory id is required.");
        }
    }
}
