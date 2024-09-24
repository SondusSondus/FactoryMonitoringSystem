using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FactoryMonitoringSystem.Application.Factories.Commands.CreateFactory
{
    public class CreateFactoryCommandValidator : AbstractValidator<CreateFactoryCommand>
    {
        public CreateFactoryCommandValidator()
        {
            RuleFor(command => command.FactoryRequet.Name)
             .NotEmpty().WithMessage("Factory name is required.")
             .MaximumLength(100).WithMessage("Factory name must not exceed 100 characters.");

            RuleFor(command => command.FactoryRequet.Location)
               .NotEmpty().WithMessage("Factory location is required.");
        }
    }
}
