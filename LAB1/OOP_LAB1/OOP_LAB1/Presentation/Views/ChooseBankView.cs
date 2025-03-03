using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Validators;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Presentation.Views;

public class ChooseBankView : IView
{
    private readonly IConsoleView _console;
    private readonly IBankService _bankService;
    private readonly IInputHandler _input;

    
    public ChooseBankView(IConsoleView console, IInputHandler input, IBankService bankService)
    {
        _console = console;
        _bankService = bankService;
        _input = input;
    }

    public PageName? NextViewName { get; set; }

    public void Execute()
    {
        _console.WriteLine("List of bank:");
        List<string> bankNames = _bankService.GetAllBankNames().ToList();
        foreach (var b in bankNames)
        {
            _console.WriteLine(b);
        }
        
        string bankName = _input.GetString("Enter bank name: ", new NameValidator());
        
        Bank bank = _bankService.GetBankByName(bankName);
        
        _console.WriteLine($"Successfully chosen bank: {bank.Name}");

        NextViewName = PageName.BankMenuPage;
        
    }
}