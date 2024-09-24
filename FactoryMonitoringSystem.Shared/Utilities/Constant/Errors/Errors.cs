
using ErrorOr;

namespace FactoryMonitoringSystem.Shared.NewFolder.Constant.Errors
{
    public static class Errors
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

        public static class General
        {
            public static Error Unexpected => Error.Unexpected(
                code: "General.Unexpected",
                description: "An unexpected error occurred.");
            public static Error DatabaseFailure => Error.Failure(
                code: "General.DatabaseFailure", 
                description: "A database error occurred.");

        }
    }
}
