using OOP_LAB1.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Application.Services;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Infrastructure.Repositories;
using OOP_LAB1.Presentation;
using OOP_LAB1.Presentation.Controllers;


var serviceProvider = new ServiceCollection()
    .AddSingleton<IAuthorizationService, AuthorizationService>()
    .AddSingleton<IUserRepository, UserRepository>()
    .AddSingleton<IEmployeeRepository, EmployeeRepository>()
    .BuildServiceProvider();


