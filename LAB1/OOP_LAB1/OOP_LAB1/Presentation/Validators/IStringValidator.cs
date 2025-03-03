namespace OOP_LAB1.Presentation.Validators;

public interface IStringValidator
{
    public bool IsValid(string stringToValidate);
    public string GetInvalidValidationString();
}