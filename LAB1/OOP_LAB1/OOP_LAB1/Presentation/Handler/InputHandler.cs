﻿using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Validators;
namespace OOP_LAB1.Presentation.Handler;

public class InputHandler : IInputHandler
{
    private readonly IConsole _console;

    public InputHandler(IConsole console) => _console = console;

    public string GetString(string prompt, IValidator validator)
    {
        _console.WriteLine(prompt);
        while (true)
        {
            var input = _console.ReadLine();
            if (validator.IsValid(input)) return input;
            _console.WriteLine(validator.GetInvalidValidationString());
        }
    }

    public int GetIntNumber(string prompt, IValidator validator)
    {
        _console.WriteLine(prompt);
        while (true)
        {
            var input = _console.ReadLine();
            if (validator.IsValid(input))
            {
                Int32.TryParse(input, out var result);
                return result;
            }
            _console.WriteLine(validator.GetInvalidValidationString());
            
        }
    }
    
    public decimal GetDecimalNumber(string prompt, IValidator validator)
    {
        _console.WriteLine(prompt);
        while (true)
        {
            var input = _console.ReadLine();
            if (validator.IsValid(input))
            {
                Decimal.TryParse(input, out var result);
                return result;
            }
            _console.WriteLine(validator.GetInvalidValidationString());
            
        }
    }
}
