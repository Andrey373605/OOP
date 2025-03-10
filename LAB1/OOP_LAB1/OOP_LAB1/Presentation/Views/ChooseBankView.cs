using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Services;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

public class ChooseBankView : IView
{
    public PageName? NextViewName { get; private set; }
    

    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsole _console;
    private readonly IContext _context;
    private readonly IBankService _bankService;

    public ChooseBankView(IInputHandler input, IAuthorizationService auth, IConsole console, IContext context, IBankService bankService)
    {
        _input = input;
        _auth = auth;
        _console = console;
        _context = context;
        _bankService = bankService;
    }
    
    public async Task Execute()
    {
        List<string> banks = await _bankService.GetAllBankNames();
        foreach (string b in banks)
        {
            _console.WriteLine(b);
        }
        var bankName = _input.GetString("Enter bank name: ", new NameValidator());
        var bank = await _bankService.GetBankByName(bankName);
        _auth.LoginBank(_context, bank);
        _console.WriteLine($"Succesfully chosen bank {bankName}");
        _context.SetCurrent(bank);

        NextViewName = PageName.ChooseRolePage;
    }
}