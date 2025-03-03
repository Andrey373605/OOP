
namespace OOP_LAB1.Presentation.Validators;

public class SeriesValidator : IStringValidator
{
    public bool IsValid(string stringToValidate)
    {
        return true;
    }

    public string GetInvalidValidationString()
    {
        return "Invalid passport series";
    }
}