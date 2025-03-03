

namespace OOP_LAB1.Presentation.Validators;

public class IdentificationNumberValidator : IStringValidator
{
    public bool IsValid(string stringToValidate)
    {
        return true;
    }

    public string GetInvalidValidationString()
    {
        return "Identification Number is invalid";
    }
}