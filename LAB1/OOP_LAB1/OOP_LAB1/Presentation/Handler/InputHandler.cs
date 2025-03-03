using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Validators;
namespace OOP_LAB1.Presentation.Handler;

public class InputHandler : IInputHandler
{
    private readonly IConsoleView _console;

    public InputHandler(IConsoleView console) => _console = console;

    public string GetString(string prompt, IStringValidator validator)
    {
        _console.WriteLine(prompt);
        while (true)
        {
            var input = _console.ReadLine();
            if (validator.IsValid(input)) return input;
            _console.WriteLine(validator.GetInvalidValidationString());
        }
    }


}