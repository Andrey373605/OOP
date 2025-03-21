﻿using System.ComponentModel;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ManagerAllTransfersPage)]
public class ManagerAllTransfersView : IView
{
    IConsole _console;
    IApplicationService _applicationService;
    private IInputHandler _input;

    public ManagerAllTransfersView(IConsole console, IApplicationService applicationService, IInputHandler input)
    {
        _console = console;
        _applicationService = applicationService;
        _input = input;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("Accounts: ");
        
        var accountId = _input.GetIntNumber("Enter account number: ", new IntValidator());
        _console.Clear();
        try
        {
            var transfers = await _applicationService.GetTransfersByAccountIdAsync(accountId);
            _console.WriteLine("Transfers: ");
            foreach (var t in transfers)
            {
                var type = accountId == t.FromAccountId ? "sending" : "receiving";
                _console.WriteLine($"Number: {t.Id}\t" +
                                   $"From: {t.FromAccountId} \t" +
                                   $"To: {t.ToAccountId}\t" +
                                   $"Amount: {t.Amount}\t" +
                                   $"Date: {t.Date.Date}\t" +
                                   $"Type: {type}");
            }
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
        }
        NextViewName = PageName.ManagerMainMenuPage;
    }
}