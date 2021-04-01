using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomErrorSample
{
    /// <summary>
    /// The sample uses a custom IErrorMessage implementation used to communicate error codes instead of fixed messages to allow the UI to format and translate them as it sees fit.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Return errors if creating a person with missing required fields.
            var invalidCreate = Person.Create(null, null, null);
            Print(invalidCreate);

            // Create a valid person
            var john = Person.Create("John", "Smith");
            Print(john);

            // Return errors when trying to apply an invalid change.
            var invalidChange = john.Value.SetLastName(null);
            Print(invalidChange.Map(_ => john.Value));

            Console.ReadLine();
        }

        private static void Print(Result<Person, IErrorMessage> personResult)
        {
            if (personResult.IsSuccess)
            {
                Console.WriteLine("Success:");
                Console.WriteLine($"{personResult.Value.FirstName} {personResult.Value.MiddleName} {personResult.Value.LastName}");
                Console.WriteLine();
            }
            else if (personResult.IsFailure && personResult.Error is InvalidInputError invalidInputError)
            {
                Console.WriteLine($"Failure:");

                var errors = invalidInputError.AllErrors
                    .OfType<InvalidInputError>()
                    .Select(error => string.Format(_errorMessages[error.Code], error.Properties.Values.ToArray()));

                Console.WriteLine(string.Join(Environment.NewLine, errors));
                Console.WriteLine();
            }
        }

        // Emulates a UI translation of the error codes to user friendly, localized error messages.
        private static Dictionary<string, string> _errorMessages = new Dictionary<string, string>
        {
            ["RequiredField"] = "The field {0} is required.",
            ["MaxLengthExceeded"] = "The field {0} can't exceed {1} characters",
        };
    }
}


