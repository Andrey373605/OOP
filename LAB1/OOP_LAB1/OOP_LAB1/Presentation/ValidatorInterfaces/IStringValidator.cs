namespace OOP_LAB1.Presentation.ValidatorInterfaces;

public interface IStringValidator
{
    public bool IsValid(string stringToValidate);
    public string GetInvalidValidationString();
}