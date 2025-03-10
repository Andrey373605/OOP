

namespace OOP_LAB1.Presentation.Validators;

public class IdentificationNumberValidator : IValidator
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