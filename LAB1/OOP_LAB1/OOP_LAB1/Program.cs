using OOP_LAB1.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Application.Services;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Infrastructure.Repositories;
using OOP_LAB1.Presentation;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Controllers;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Views;

// Настройка DI
var serviceProvider = new ServiceCollection()
    // регистрация сервисов
    .AddSingleton<IAuthorizationService, AuthorizationService>()
    .AddSingleton<IBankService, BankService>()
    
    // регистрация консоли 
    .AddSingleton<IConsoleView, ConsoleView>()
    
    // регистрация handler
    .AddSingleton<IInputHandler, InputHandler>()
    
    // регистрация navigator
    .AddSingleton<INavigator, Navigator>()
    
    // регистрация репозиториев
    .AddSingleton<IUserRepository, UserRepository>()
    .AddSingleton<IEmployeeRepository, EmployeeRepository>()
    
    // регистрация view
    .AddTransient<MainMenuView>()
    .AddTransient<RegistrationView>()
    .AddTransient<ChooseBankView>()
    
    // сборка
    .BuildServiceProvider();


var navigator = serviceProvider.GetService<INavigator>();

navigator.RegisterView(PageName.BankChoosePage, serviceProvider.GetService<ChooseBankView>());
navigator.RegisterView(PageName.MainMenuPage, serviceProvider.GetService<MainMenuView>());
navigator.RegisterView(PageName.RegistrationPage, serviceProvider.GetService<RegistrationView>());


navigator.Run(PageName.MainMenuPage);