namespace Server.Exceptions;

public class AuthValidationException : Exception
{
    public Dictionary<string, string[]> Errors { get; }

    public AuthValidationException(Dictionary<string, string[]> errors)
    {
        Errors = errors;
    }
}