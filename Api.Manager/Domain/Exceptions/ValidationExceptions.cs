using FluentValidation.Results;

namespace Api.Manager.Domain.Exceptions
{
    public class ValidationExceptions : Exception
    {
        public ValidationExceptions() : base("One or more validation errors have occurred.")
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; }

        public ValidationExceptions(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var item in failures)
            {
                Errors.Add(item.ErrorMessage);
            }
        }
    }
}
