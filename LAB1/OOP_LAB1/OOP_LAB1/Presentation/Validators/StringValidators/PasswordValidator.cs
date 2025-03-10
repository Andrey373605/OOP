
namespace OOP_LAB1.Presentation.Validators;

public class PasswordValidator : IValidator
{
    public bool IsValid(string stringToValidate)
    {
        return true;
    }

    public string GetInvalidValidationString()
    {
        return "Password is too easy";
    }
}