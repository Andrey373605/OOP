namespace OOP_LAB1.Presentation.Validators;

public class IdValidator : IValidator
{
    public bool IsValid(string stringToValidate)
    {
        return Int16.TryParse(stringToValidate, out Int16 parsedStringToValidate);
        
    }

    public string GetInvalidValidationString()
    {
        return "The ID consists only of numbers";
    }
}