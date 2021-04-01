using CSharpFunctionalExtensions;

namespace CustomErrorSample
{
    /// <summary>
    /// This is a simplified example demonstrating the re-use and combining of errors, normally I would create value objects for the name parameters.
    /// </summary>
    public class Person
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public static Result<Person, IErrorMessage> Create(string firstName, string lastName, string middleName = null)
        {
            var person = new Person();

            // A custom Combine method is required because the standard Combine implementation would return a Result<Empty[], IErrorMessage> which is not the desired outcome.
            var results = Empty.Combine(new[] {
                person.SetFirstName(firstName),
                person.SetMiddleName(middleName),
                person.SetLastName(lastName)
            });

            return results.Bind<Empty, Person, IErrorMessage>(_ => person);
        }

        public Result<Empty, IErrorMessage> SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Failure<Empty, IErrorMessage>(MainErrors.RequiredFieldError("first name"));

            FirstName = firstName;
            return Empty.Instance;
        }

        public Result<Empty, IErrorMessage> SetMiddleName(string middleName)
        {
            MiddleName = middleName;
            return Empty.Instance;
        }

        public Result<Empty, IErrorMessage> SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Failure<Empty, IErrorMessage>(MainErrors.RequiredFieldError("last name"));

            LastName = lastName;
            return Empty.Instance;
        }
    }

}


