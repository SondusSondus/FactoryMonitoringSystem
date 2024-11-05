using FluentValidation;

namespace FactoryMonitoringSystem.Application.UserManagement.Queries.GetUserById
{
    internal class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(user => user.Id)
            .NotEmpty().WithMessage("User id is required.");
        }
    }
}
