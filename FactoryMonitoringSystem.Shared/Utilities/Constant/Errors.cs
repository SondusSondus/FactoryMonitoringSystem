
using ErrorOr;

namespace FactoryMonitoringSystem.Shared.Utilities.Constant
{
    public static partial class Errors
    {
        public static class FactoryError
        {
            public static Error NotFound => Error.NotFound(
                code: "Factory.NotFound",
                description: "The requested factory was not found.");

            public static Error DuplicateMachine => Error.Conflict(
                code: "Factory.DuplicateMachine",
                description: "The factory already has a machine of this type.");
        }

        public static class MachineError
        {
            public static Error NotFound => Error.NotFound(
                code: "Machine.NotFound",
                description: "The requested machine was not found.");
        } 
        public static class SensorError
        {
            public static Error NotFound => Error.NotFound(
                code: "Sensor.NotFound",
                description: "The requested sensor was not found.");
        }
        public static class AuthError
        {
            public static Error UserNotFound => Error.NotFound(
                code: " Auth.UserNotFound",
                description: "The user login  was not found.");
            public static readonly Error InvalidCredentials = Error.Validation(
            code: "Auth.InvalidCredentials",
            description: "The username or password is incorrect.");

            public static readonly Error EmailNotVerified = Error.Validation(
                code: "Auth.EmailNotVerified",
                description: "You must verify your email before logging in.");

            public static Error AccountLockedOut(DateTime lockoutEnd) => Error.Conflict(
                code: "Auth.AccountLockedOut",
                description: $"Your account is locked until {lockoutEnd.ToString("u")}.");
        }

        public static class General
        {
            public static Error Unexpected => Error.Unexpected(
                code: "General.Unexpected",
                description: "An unexpected error occurred.");
            public static Error DatabaseFailure => Error.Failure(
                code: "General.DatabaseFailure",
                description: "A database error occurred.");

            public static Error TokenGeneratorFailure => Error.Failure(
                code: "General.TokenGeneratorFailure",
                description: "A token  Generator error occurred.");
            
            public static Error TokenRefresh => Error.Failure(
                code: "General.TokenRefresh",
                description: "A Token refresh not found.");

        }
        public static class UserError
        {
            public static readonly Error UsernameAlreadyExists = Error.Conflict(
                code: "User.UsernameAlreadyExists",
                description: "The username is already taken.");

            public static readonly Error EmailAlreadyExists = Error.Conflict(
                code: "User.EmailAlreadyExists",
                description: "The email is already registered.");

            public static readonly Error UserNotFound = Error.NotFound(
                code: "User.NotFound",
                description: "User not found.");

            public static readonly Error PasswordNotMatch = Error.NotFound(
                code: "User.PasswordNotMatch",
                description: "Password not match with hashed password.");

            public static readonly Error InvalidOrExpiredVerificationCode = Error.Validation(
                code: "User.InvalidOrExpiredVerificationCode",
                description: "The verification code is invalid or has expired.");

        }
    }
}
