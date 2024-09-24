using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FactoryMonitoringSystem.Api.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected IServiceProvider Resolver => HttpContext.RequestServices;
        protected T GetService<T>()
        {
            return Resolver.GetService<T>();
        }
        protected IMapper Mapper => GetService<IMapper>();

        protected IMediator Mediator => GetService<IMediator>();
        protected ISender Sender => GetService<ISender>();
        protected IPublisher Publisher => GetService<IPublisher>();
        protected ILogger Logger => GetService<ILogger>();
        //protected string LoggedInUserId => HttpContext.User.Identity.GetUserId();
       // protected ICurrentUser CurrentUser => GetService<ICurrentUser>();
        protected ActionResult Problem(List<Error> errors)
        {
            if (errors.Count is 0)
            {
                return Problem();
            }

            if (errors.All(error => error.Type == ErrorType.Validation))
            {
                return ValidationProblem(errors);
            }

            return Problem(errors[0]);
        }

        private ObjectResult Problem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError,
            };

            return Problem(statusCode: statusCode, title: error.Description);
        }

        private ActionResult ValidationProblem(List<Error> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();

            errors.ForEach(error => modelStateDictionary.AddModelError(error.Code, error.Description));

            return ValidationProblem(modelStateDictionary);
        }
    }
}
