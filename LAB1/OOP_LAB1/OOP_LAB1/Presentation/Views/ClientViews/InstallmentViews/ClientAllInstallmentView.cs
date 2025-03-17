using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ClientAllInstallmentPage)]
public class ClientAllInstallmentView : IView
{
    private readonly IConsole _console;
    private readonly IApplicationService _applicationService;

    public ClientAllInstallmentView(IConsole console, IApplicationService applicationService)
    {
        _console = console;
        _applicationService = applicationService;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.Clear();
        _console.WriteLine("Installments:");
        var accounts = await _applicationService.GetCurrentClientInstallments();
        if (accounts.Any())
        {
            foreach (var l in accounts)
            {
                _console.WriteLine($"Amount: {l.Amount} \t " +
                                   $"Number of payments: {l.NumberOfPayments} \t " +
                                   $"Rest amount of payments: {l.RestMonth} \t ");
            }
        }
        

        NextViewName = PageName.ClientInstallmentMenuPage;

    }
}