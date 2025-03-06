using System.Text.RegularExpressions;

namespace OOP_LAB1.Presentation.Validators;

public class EmailValidator : IStringValidator
{
    public bool IsValid(string stringToValidate)
    {
        /*string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(stringToValidate);*/
        return true;
    }

    public string GetInvalidValidationString()
    {
        return "Invalid email address";
    }
}