

namespace OOP_LAB1.Presentation.Validators;

public class NameValidator : IStringValidator
{
    public bool IsValid(string stringToValidate)
    {
        return true;
    }

    public string GetInvalidValidationString()
    {
        return "Name is invalid";
    }
}