namespace OOP_LAB1.Presentation.Validators;

public class MonthValidator : IValidator
{
    public bool IsValid(string stringToValidate)
    {
        return Int16.TryParse(stringToValidate, out Int16 parsedStringToValidate) &&
               parsedStringToValidate > 0 &&
               parsedStringToValidate <= 24;
        
    }

    public string GetInvalidValidationString()
    {
        return "To match duration";
    }
}