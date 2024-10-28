using ErrorOr;
using FluentValidation;
using MediatR;


namespace FactoryMonitoringSystem.Api.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : IRequest<TResponse>
      where TResponse : IErrorOr
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }



        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationFailures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (validationFailures.Any())
            {
                var errors = validationFailures.Select(f => Error.Validation(f.PropertyName, f.ErrorMessage)).ToList();
                return (dynamic)errors;
            }

            return await next();
        }
    }
}
