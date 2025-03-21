using System.Reflection;
using OOP_LAB1.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Application.Services;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Infrastructure.Repositories;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Registration;
using OOP_LAB1.Presentation.Views;
using OOP_LAB1.Presentation.Views.AdministratorViews;
using OOP_LAB1.Presentation.Views.SalaryProjectViews;
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
    .AddSingleton<IClientService, ClientService>()
    .AddSingleton<IEmployeeService, EmployeeService>()
    .AddSingleton<ISalaryProjectService, SalaryProjectService>()
    .AddSingleton<IEnterpriseService, EnterpriseService>()
    
    // регистрация контекста
    .AddSingleton<IContext, Context>()
    
    //
    .AddScoped<IDataBaseHelper>(provider =>
            new DatabaseHelper("Data Source=../../../Infrastructure/DataBase/sample.db;"))
    
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
    .AddSingleton<ISalaryProjectRepository, SalaryProjectRepository>()
    .AddSingleton<IEnterpriseRepository, EnterpriseRepository>()
    
    // регистрация view
    .AddTransient<MainMenuView>()
    .AddTransient<RegistrationClientView>()
    .AddTransient<RegisterInBankView>()
    .AddTransient<RegistrationUserView>()
    .AddTransient<RegistrationEmployeeView>()
    .AddTransient<LoginUserView>()
    .AddTransient<ChooseRoleView>()
    .AddTransient<ChooseBankView>()
    .AddTransient<LoginClientView>()
    
    .AddTransient<ClientMainMenuView>()
    .AddTransient<LogOutView>()
    //transactions view
    .AddTransient<ClientTransactionMenuView>()
    .AddTransient<ClientDepositAccountView>()
    .AddTransient<ClientTransferAccountView>()
    .AddTransient<ClientWithdrawAccountView>()
    .AddTransient<ClientAllTransfersView>()
    .AddTransient<ClientAllDepositsView>()
    .AddTransient<ClientAllWithdrawsView>()
    
    //account views
    .AddTransient<ClientAccountMenuView>()
    .AddTransient<ClientAllAccountsView>()
    .AddTransient<ClientWithdrawAccountView>()
    .AddTransient<ClientFreezeAccountView>()
    .AddTransient<ClientUnfreezeAccountView>()
    .AddTransient<ClientCreateAccountView>()
    
    //loan views
    .AddTransient<ClientLoanMenuView>()
    .AddTransient<ClientLoanRequestView>()
    .AddTransient<ClientAllLoanView>()
    
    //installment views
    .AddTransient<ClientInstallmentMenuView>()
    .AddTransient<ClientInstallmentRequestView>()
    .AddTransient<ClientAllInstallmentView>()
    
    //client salary
    .AddTransient<ClientSalaryRequestView>()
    
    //operator
    .AddTransient<OperatorMainMenuView>()
    .AddTransient<OperatorCancelTransferView>()
    .AddTransient<OperatorAllDepositsView>()
    .AddTransient<OperatorAllWithdrawsView>()
    .AddTransient<OperatorAllTransfersView>()
    .AddTransient<OperatorApproveSalaryProjectView>()
    
    //manager
    .AddTransient<ManagerMainMenuView>()
    .AddTransient<ManagerCancelTransferView>()
    .AddTransient<ManagerAllDepositsView>()
    .AddTransient<ManagerAllWithdrawsView>()
    .AddTransient<ManagerAllTransfersView>()
    .AddTransient<ManagerApproveSalaryProjectView>()
    .AddTransient<ManagerApproveInstallmentView>()
    .AddTransient<ManagerApproveLoanView>()
    .AddTransient<ManagerApproveClientRegistrationView>()
    
    //admin
    .AddTransient<AdministratorMainMenuView>()
    
    //specialist
    .AddTransient<SpecialistMainMenuView>()
    .AddTransient<SpecialistProjectApplicationView>()
    .AddTransient<SpecialistSalaryRequestView>()
    
    .AddTransient<LoginEmployeeView>()
    .AddTransient<ExitView>()
    
    // сборка
    .BuildServiceProvider();


var navigator = serviceProvider.GetService<INavigator>();

ViewRegistrar.RegisterViews(serviceProvider, navigator);

navigator.Run(PageName.MainMenuPage);