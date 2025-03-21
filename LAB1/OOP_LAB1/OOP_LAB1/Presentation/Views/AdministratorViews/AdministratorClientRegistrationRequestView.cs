using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.AdministratorClientRegistrationRequestPage)]
public class AdministratorClientRegistrationRequestView : IView
{
    private readonly IApplicationService _applicationService;
    private readonly IConsole _console;
    private readonly IInputHandler _input;
    private readonly IClientService _clientService;

    public AdministratorClientRegistrationRequestView(IApplicationService applicationService, IConsole console, IInputHandler input, IClientService clientService)
    {
        _applicationService = applicationService;
        _console = console;
        _input = input;
        _clientService = clientService;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("Registration requests");
        var clients = await _clientService.GetClientRegistrationRequests();
        foreach (var c in clients)
        {
            _console.WriteLine($"Id: {c.Id}\t" +
                               $"Full name: {c.FirstName} {c.LastName} {c.MiddleName}\t" +
                               $"Passport: {c.PassportSeries}{c.IdentificationNumber}\t" +
                               $"phone: {c.Phone}");
        }
        
        _console.WriteLine("1. Approve registration");
        _console.WriteLine("2. Reject registration");
        _console.WriteLine("3. Return back");

        var choice = _input.GetNumberVariant(3);
        if (choice == "1")
        {
            var id = _input.GetIntNumber("Enter Id client: ", new IntValidator());
            _console.Clear();
            try
            {
                await _clientService.ApproveClientRegistration(id);
                _console.WriteLine("Registration client approved");
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
            }
        }
        else if (choice == "2")
        {
            var id = _input.GetIntNumber("Enter Id client: ", new IntValidator());
            _console.Clear();
            try
            {
                await _clientService.RejectClientRegistration(id);
                _console.WriteLine("Client registration rejected");
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
            }
        }
        
        NextViewName = PageName.AdministratorMainMenuPage;
    }
}