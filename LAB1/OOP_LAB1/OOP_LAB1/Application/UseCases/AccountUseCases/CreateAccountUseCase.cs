using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.UseCases.AccountUseCases;

public class CreateAccountUseCase
{
    private readonly IAccountService _accountService;

    public CreateAccountUseCase(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public Account Execute(AccountType type, int ownerId)
    {
        return _accountService.CreateAccount(type, ownerId);
    }
}