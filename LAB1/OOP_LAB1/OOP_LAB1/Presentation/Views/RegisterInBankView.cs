using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Validators;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Presentation.Views;

public class RegisterInBankView : IView
{
    private readonly IConsoleView _console;
    private readonly IBankService _bankService;
    private readonly IInputHandler _input;
    private readonly IContext _context;

    
    public RegisterInBankView(IConsoleView console, IInputHandler input, IBankService bankService)
    {
        _console = console;
        _bankService = bankService;
        _input = input;
    }

    public PageName? NextViewName { get; set; }

    public void Execute()
    {
        _console.WriteLine("List of bank:");
        List<string> bankNames = _bankService.GetAllBankNames().GetAwaiter().GetResult();
        foreach (var b in bankNames)
        {
            _console.WriteLine(b);
        }
        
        _console.WriteLine("Choose bank:");
        string bankName = _console.ReadLine();
        
        Bank bank = _bankService.GetBankByName(bankName).GetAwaiter().GetResult();
        
        
        _console.WriteLine($"Successfully chosen bank: {bank.Name}");
        
        
        string role = _console.ReadLine();
        NextViewName = role switch
        {
            "1" => PageName.MainMenuPage,
            "2" => PageName.LoginPage,
            _ => NextViewName
        };
        
        _console.WriteLine("Successfully chosen role");
        

        NextViewName = PageName.MainMenuPage;
        
    }
}