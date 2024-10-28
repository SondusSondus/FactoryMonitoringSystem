using FluentValidation;

namespace FactoryMonitoringSystem.Application.Factories.Queries.GetFactoryById
{
    public class GetFactoryByIdQueryValidator : AbstractValidator<GetFactoryByIdQuery>
    {
        public GetFactoryByIdQueryValidator()
        {
            RuleFor(factory => factory.id)
                  .NotEmpty().WithMessage("Factory id is required.");
        }
    }
}
