namespace KyrgyzTest.Application.Validations;

public class Message
{
    public const string REQUIRED = "The field {0} cannot be empty";
    public const string MAX_LENGTH = "The field {0} cannot be longer than {1} characters";
    public const string EMAIL = "The field {0} must contain a valid email address";
    public const string ONLY_LETTERS = "The field {0} must contain only letters";
    public const string POSITIVE_NUMBER = "Id must be a positive number";
}