﻿using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ClientDepositAccountPage)]
public class ClientDepositAccountView : IView
{
    private readonly IApplicationService _applicationService;
    private readonly IConsole _console;
    private readonly IInputHandler _input;

    public ClientDepositAccountView(IApplicationService applicationService, IConsole console, IInputHandler input)
    {
        _applicationService = applicationService;
        _console = console;
        _input = input;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("Accounts:");
        var accounts = await _applicationService.GetCurrentClientAccounts();
        foreach (var a in accounts)
        {
            _console.WriteLine($"Id: {a.Id} \t Balance: {a.Balance} \t Active: {a.Status.ToString()} \t Type: {a.AccountType.ToString()}" );
        }
        var accountId = _input.GetIntNumber("Enter Account Id", new IntValidator());
        
        var sum = _input.GetDecimalNumber("Enter Sum Of Amount", new SumValidator());

        _console.Clear();
        try
        {
            await _applicationService.DepositAccount(accountId, sum);
            _console.WriteLine("Success!");
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
        }

        NextViewName = PageName.ClientTransactionMenuPage;

    }
}