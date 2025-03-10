namespace OOP_LAB1.Presentation.Validators;

public interface IValidator
{
    public bool IsValid(string stringToValidate);
    public string GetInvalidValidationString();
}