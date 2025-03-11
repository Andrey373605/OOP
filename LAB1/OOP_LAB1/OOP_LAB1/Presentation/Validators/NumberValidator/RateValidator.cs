namespace OOP_LAB1.Presentation.Validators;

public class RateValidator : IValidator
{
    public bool IsValid(string stringToValidate)
    {
        return Int16.TryParse(stringToValidate, out Int16 parsedStringToValidate) &&
               parsedStringToValidate > 0 &&
               parsedStringToValidate <= 100;
        
    }

    public string GetInvalidValidationString()
    {
        return "To big rate";
    }
}