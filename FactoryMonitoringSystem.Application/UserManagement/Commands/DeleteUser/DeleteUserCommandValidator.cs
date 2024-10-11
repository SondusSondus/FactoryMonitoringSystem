﻿using FluentValidation;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.DeleteUser
{
    public class DeleteUserCommandValidator :AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {

            RuleFor(user => user.UserId)
                       .NotEmpty().WithMessage("User id is required.");
        }
    }
}
