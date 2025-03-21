﻿using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Validators;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.RegisterInBankPage)]
public class RegisterInBankView : IView
{
    private readonly IConsole _console;
    private readonly IBankService _bankService;
    private readonly IInputHandler _input;


    
    public RegisterInBankView(IConsole console, IInputHandler input, IBankService bankService)
    {
        _console = console;
        _bankService = bankService;
        _input = input;
    }

    public PageName? NextViewName { get; set; }

    public async Task Execute()
    {
        _console.WriteLine("List of bank:");
        List<string> bankNames = await _bankService.GetAllBankNames();
        foreach (var b in bankNames)
        {
            _console.WriteLine(b);
        }
        
        _console.WriteLine("Choose bank:");
        string bankName = _console.ReadLine();
        
        Bank bank = await _bankService.GetBankByName(bankName);
        
        _console.Clear();
        _console.WriteLine($"Successfully chosen bank: {bank.Name}");


        string role = _input.GetNumberVariant(2);
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