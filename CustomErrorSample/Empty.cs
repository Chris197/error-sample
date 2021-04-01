using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace CustomErrorSample
{
    /// <summary>
    /// A placeholder type used for Results with custom Error messages that return no value. Name Empty chosen to prevent naming conflicts with the more common 'Unit' term.
    /// </summary>
    public readonly struct Empty
    {
        public static Empty Instance => default;

        // A custom Combine method is required because the standard Combine implementation would return a Result<Empty[], IErrorMessage> which is not the desired outcome.        
        public static Result<Empty, E> Combine<E>(IEnumerable<Result<Empty, E>> results) where E : ICombine
        {
            var failedResults = results.Where(x => x.IsFailure).Select(r => r.Error).ToList();

            if (failedResults.Count == 0)
                return Result.Success<Empty, E>(default);

            var error = failedResults.Aggregate((E x, E y) => (E)x.Combine(y));
            return Result.Failure<Empty, E>(error);
        }
    }
}


