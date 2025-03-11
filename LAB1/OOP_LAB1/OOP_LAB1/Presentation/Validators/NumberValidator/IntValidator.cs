namespace OOP_LAB1.Presentation.Validators;

public class IntValidator : IValidator
{
    public bool IsValid(string stringToValidate)
    {
        return Int16.TryParse(stringToValidate, out Int16 parsedStringToValidate) && parsedStringToValidate > 0;
        
    }

    public string GetInvalidValidationString()
    {
        return "The ID consists only of numbers";
    }
}