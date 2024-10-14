using FluentValidation;

namespace FactoryMonitoringSystem.Application.Auth.Commands.CheckUserByRefrshToken
{
    public class CheckUserByRefrshTokenCommandValidator :AbstractValidator<CheckUserByRefrshTokenCommand>
    {
        public CheckUserByRefrshTokenCommandValidator()
        {
            RuleFor(token => token.RefrshToken)
              .NotEmpty().WithMessage("Refresh token is required");
        }
    }
}
