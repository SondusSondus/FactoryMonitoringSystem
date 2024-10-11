using FluentValidation;

namespace FactoryMonitoringSystem.Application.Machines.Queries.GetMachineById
{
    public class GetMachineByIdQueryValidator : AbstractValidator<GetMachineByIdQuery>
    {
        public GetMachineByIdQueryValidator()
        {
            RuleFor(machine => machine.Id)
                 .NotEmpty().WithMessage("Machine id is required.");
        }
    }
}
