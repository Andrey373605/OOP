using OOP_LAB1.Presentation.Validators;
namespace OOP_LAB1.Presentation.Handler;

public interface IInputHandler
{
    public string GetString(string prompt, IValidator validator);
    public int GetIntNumber(string prompt, IValidator validator);
    public decimal GetDecimalNumber(string prompt, IValidator validator);
}