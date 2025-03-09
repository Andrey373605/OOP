using System.Diagnostics;
using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

public class RegistrationEmployeeView : IView
{
    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsole _console;
    private readonly IContext _context;
    
    public PageName? NextViewName { get; private set; }

    public RegistrationEmployeeView(IInputHandler input, IConsole console, IAuthorizationService authService, IContext context)
    {
        _input = input;
        _auth = authService;
        _console = console;
        _context = context;
    }

    public void Execute()
    {
        _console.WriteLine("Choose role:");
        _console.WriteLine("1. Operator");
        _console.WriteLine("2. Manager");
        _console.WriteLine("3. Specialist");
        _console.WriteLine("4. Administrator");
        string choice = _console.ReadLine();

        UserRole role = choice switch
        {
            "1" => UserRole.Operator,
            "2" => UserRole.Manager,
            "3" => UserRole.ExternalSpecialist,
            "4" => UserRole.Administrator,
            _ => UserRole.Operator
        };
        
        _console.WriteLine($"Succesfully choosen role {role.ToString()}");
        
        try
        {

            _auth.RegisterEmployeeAsync(role).GetAwaiter().GetResult();
            NextViewName = PageName.RegisterInBankPage;
            _console.WriteLine("Registration successful!");
        }
        catch (Exception ex)
        {
            _console.WriteLine($"Error: {ex.Message}");
        }
        
    }
}