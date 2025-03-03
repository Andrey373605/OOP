using OOP_LAB1.Presentation.Validators;
namespace OOP_LAB1.Presentation.Handler;

public interface IInputHandler
{
    public string GetString(string prompt, IStringValidator validator);
}