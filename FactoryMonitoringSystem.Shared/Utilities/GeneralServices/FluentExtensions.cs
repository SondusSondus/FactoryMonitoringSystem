using ErrorOr;

namespace FactoryMonitoringSystem.Shared.Utilities.GeneralServices
{
    public static class FluentExtensions
    {
        // Validation method for Task<T>
        public static async Task<ErrorOr<T>> Validate<T>(
            this Task<T> task,
            Func<T, bool> predicate,
            Func<ErrorOr<T>> errorFactory)
        {
            var result = await task;
            return predicate(result) ? result : errorFactory();
        }

        // Validation method for Task<ErrorOr<T>>
        public static async Task<ErrorOr<T>> Validate<T>(
            this Task<ErrorOr<T>> task,
            Func<T, bool> predicate,
            Func<ErrorOr<T>> errorFactory)
        {
            var result = await task;
            if (result.IsError)
            {
                return result; // Return the existing error
            }

            // Apply the predicate check to the unwrapped value
            return predicate(result.Value) ? result : errorFactory();
        }
        
        // Validation method for  Func<Task<ErrorOr<T>>>

        public static async Task<ErrorOr<T>> Validate<T>(
            this Task<ErrorOr<T>> task,
            Func<T, bool> predicate,
            Func<Task<ErrorOr<T>>> errorFactory)
        {
            var result = await task;
            if (result.IsError)
            {
                return result; // Return the existing error
            }
            // Apply the predicate check to the unwrapped value
            return predicate(result.Value) ? result : await errorFactory();
        }

        // Map method for Task<ErrorOr<T>> with transformation
        public static async Task<ErrorOr<TResult>> Map<T, TResult>(
            this Task<ErrorOr<T>> task,
            Func<T, Task<TResult>> mapFunc)
        {
            var result = await task;
            if (result.IsError)
            {
                return result.Errors; // Forward any errors
            }

            var mappedResult = await mapFunc(result.Value); // Transform the value
            return mappedResult;
        }
    }

}


