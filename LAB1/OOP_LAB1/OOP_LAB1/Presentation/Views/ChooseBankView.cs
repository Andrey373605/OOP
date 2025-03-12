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
    private readonly IConsole _console;
    private readonly IApplicationService _applicationService;
    private readonly IBankService _bankService;

    public ChooseBankView(IInputHandler input, IConsole console, IApplicationService applicationService, IBankService bankService)
    {
        _input = input;
        _console = console;
        _applicationService = applicationService;
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
        

        try
        {
            var bank = await _bankService.GetBankByName(bankName);
            _applicationService.LoginBank(bank);
            _console.WriteLine($"Succesfully chosen bank {bankName}");
            NextViewName = PageName.ChooseRolePage;
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
            NextViewName = PageName.ChooseBankPage;
            
        }
        

        
    }
}