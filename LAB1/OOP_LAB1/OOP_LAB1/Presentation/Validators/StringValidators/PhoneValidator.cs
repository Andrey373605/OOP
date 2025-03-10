
namespace OOP_LAB1.Presentation.Validators;

public class PhoneValidator : IValidator
{
    public bool IsValid(string stringToValidate)
    {
        return true;
    }

    public string GetInvalidValidationString()
    {
        return "Invalid phone number";
    }
}