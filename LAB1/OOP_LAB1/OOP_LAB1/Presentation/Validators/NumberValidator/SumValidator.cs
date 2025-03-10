using System.Text.RegularExpressions;

namespace OOP_LAB1.Presentation.Validators;

public class SumValidator : IValidator
{
    public bool IsValid(string stringToValidate)
    {
        return Regex.IsMatch(stringToValidate, @"^\d+(\.\d{0,2})?$");
    }

    public string GetInvalidValidationString()
    {
        return "Sum must be a positive number";
    }
}