using OOP_LAB1.Infrastructure.Data;
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
using OOP_LAB1.Presentation.Views.ClientViews;
using Console = OOP_LAB1.Presentation.Console.Console;

// Настройка DI
var serviceProvider = new ServiceCollection()
    // регистрация сервисов
    .AddSingleton<IAuthorizationService, AuthorizationService>()
    .AddSingleton<IBankService, BankService>()
    .AddSingleton<IAccountService, AccountService>()
    .AddSingleton<IApplicationService, ApplicationService>()
    .AddSingleton<ITransactionService, TransactionService>()
    .AddSingleton<ILoanService, LoanService>()
    .AddSingleton<IInstallmentService, InstallmentService>()
    
    // регистрация контекста
    .AddSingleton<IContext, Context>()
    
    // регистрация консоли 
    .AddSingleton<IConsole, Console>()
    
    // регистрация handler
    .AddSingleton<IInputHandler, InputHandler>()
    
    // регистрация navigator
    .AddSingleton<INavigator, Navigator>()
    
    // регистрация репозиториев
    .AddSingleton<IUserRepository, UserRepository>()
    .AddSingleton<IClientRepository, ClientRepository>()
    .AddSingleton<IAccountRepository, AccountRepository>()
    .AddSingleton<IEmployeeRepository, EmployeeRepository>()
    .AddSingleton<IBankRepository, BankRepository>()
    .AddSingleton<ITransactionRepository, TransactionRepository>()
    .AddSingleton<IInstallmentRepository, InstallmentRepository>()
    .AddSingleton<ILoanRepository, LoanRepository>()
    
    // регистрация view
    .AddTransient<MainMenuView>()
    .AddTransient<RegistrationClientView>()
    .AddTransient<RegisterInBankView>()
    .AddTransient<RegistrationUserView>()
    .AddTransient<RegistrationEmployeeView>()
    .AddTransient<LoginUserView>()
    .AddTransient<ChooseRoleView>()
    .AddTransient<ChooseBankView>()
    .AddTransient<ClientMainMenuView>()
    .AddTransient<LoginClientView>()
    .AddTransient<ClientAllAccountsView>()
    .AddTransient<ClientCreateAccountView>()
    .AddTransient<ClientDepositAccountView>()
    .AddTransient<ClientTransferAccountView>()
    .AddTransient<ClientWithdrawAccountView>()
    .AddTransient<ClientFreezeAccountView>()
    .AddTransient<ClientUnfreezeAccountView>()
    .AddTransient<ClientLoanRequestView>()
    .AddTransient<ClientInstallmentRequestView>()
    .AddTransient<ExitView>()
    
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
navigator.RegisterView(PageName.ClientMainMenuPage, serviceProvider.GetService<ClientMainMenuView>());
navigator.RegisterView(PageName.ExitPage, serviceProvider.GetService<ExitView>());
navigator.RegisterView(PageName.LoginClientPage, serviceProvider.GetService<LoginClientView>());
navigator.RegisterView(PageName.ClientAllAccountsPage, serviceProvider.GetService<ClientAllAccountsView>());
navigator.RegisterView(PageName.ClientCreateAccountPage, serviceProvider.GetService<ClientCreateAccountView>());
navigator.RegisterView(PageName.ClientDepositAccountPage, serviceProvider.GetService<ClientDepositAccountView>());
navigator.RegisterView(PageName.ClientTransferAccountPage, serviceProvider.GetService<ClientTransferAccountView>());
navigator.RegisterView(PageName.ClientWithdrawAccountPage, serviceProvider.GetService<ClientWithdrawAccountView>());
navigator.RegisterView(PageName.ClientFreezeAccountPage, serviceProvider.GetService<ClientFreezeAccountView>());
navigator.RegisterView(PageName.ClientUnfreezeAccountPage, serviceProvider.GetService<ClientUnfreezeAccountView>());
navigator.RegisterView(PageName.ClientLoanRequestPage, serviceProvider.GetService<ClientLoanRequestView>());
navigator.RegisterView(PageName.CleintInstallmentRequstPage, serviceProvider.GetService<ClientInstallmentRequestView>());


navigator.Run(PageName.MainMenuPage);