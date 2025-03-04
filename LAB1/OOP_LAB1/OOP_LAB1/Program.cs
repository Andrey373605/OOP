﻿using OOP_LAB1.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Application.Services;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Infrastructure.Repositories;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Views;

// Настройка DI
var serviceProvider = new ServiceCollection()
    // регистрация сервисов
    .AddSingleton<IAuthorizationService, AuthorizationService>()
    .AddSingleton<IBankService, BankService>()
    .AddSingleton<IAccountService, AccountService>()
    
    // регистрация контекста
    .AddSingleton<IContext, Context>()
    
    // регистрация консоли 
    .AddSingleton<IConsoleView, ConsoleView>()
    
    // регистрация handler
    .AddSingleton<IInputHandler, InputHandler>()
    
    // регистрация navigator
    .AddSingleton<INavigator, Navigator>()
    
    // регистрация репозиториев
    .AddSingleton<IUserRepository, UserRepository>()
    .AddSingleton<IClientRepository, ClientRepository>()
    
    // регистрация view
    .AddTransient<MainMenuView>()
    .AddTransient<RegistrationClientView>()
    .AddTransient<RegisterInBankView>()
    .AddTransient<RegistrationUserView>()
    .AddTransient<RegistrationEmployeeView>()
    .AddTransient<LoginUserView>()
    .AddTransient<ChooseRoleView>()
    .AddTransient<ChooseBankView>()
    
    // сборка
    .BuildServiceProvider();


var navigator = serviceProvider.GetService<INavigator>();

navigator.RegisterView(PageName.RegisterInBankPage, serviceProvider.GetService<RegisterInBankView>());
navigator.RegisterView(PageName.MainMenuPage, serviceProvider.GetService<MainMenuView>());
navigator.RegisterView(PageName.RegistrationUserPage, serviceProvider.GetService<RegistrationUserView>());
navigator.RegisterView(PageName.RegistrationClientPage, serviceProvider.GetService<RegistrationClientView>());
navigator.RegisterView(PageName.RegistrationEmployeePage, serviceProvider.GetService<RegistrationEmployeeView>());
navigator.RegisterView(PageName.LoginUserPage, serviceProvider.GetService<LoginUserView>());
navigator.RegisterView(PageName.ChooseRolePage, serviceProvider.GetService<ChooseRoleView>());
navigator.RegisterView(PageName.ChooseBankPage, serviceProvider.GetService<ChooseBankView>());


navigator.Run(PageName.MainMenuPage);