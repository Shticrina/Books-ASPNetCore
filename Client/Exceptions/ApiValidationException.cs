using Shared.Responses;

namespace Client.Exceptions;

public class ApiValidationException : Exception
{
    public ValidationProblem ValidationProblem { get; }

    public ApiValidationException(ValidationProblem validationProblem)
        : base(validationProblem.Title)
    {
        ValidationProblem = validationProblem;
    }
}