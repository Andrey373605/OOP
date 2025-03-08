﻿using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

public class LoginUserView : IView
{
    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsole _console;
    private readonly IContext _context;

    public LoginUserView(IInputHandler input, IAuthorizationService auth, IConsole console, IContext context)
    {
        _input = input;
        _auth = auth;
        _console = console;
        _context = context;
    }
    public PageName? NextViewName { get; private set; }
    public void Execute()
    {
        string email = _input.GetString("Email: ", new EmailValidator());
        string password = _input.GetString("Password: ", new PasswordValidator());

        try
        {
            _auth.AuthenticateUserAsync(_context, email, password).GetAwaiter().GetResult();
            _console.WriteLine($"Successfully logged in");
            NextViewName = PageName.ChooseBankPage;
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
            NextViewName = PageName.MainMenuPage;
        }
        
        
    }
}