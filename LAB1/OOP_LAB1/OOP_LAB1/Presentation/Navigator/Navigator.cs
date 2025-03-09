using OOP_LAB1.Presentation.Views;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;

namespace OOP_LAB1.Presentation.Navigator;

public class Navigator : INavigator
{
    private readonly IConsole _console;
    private readonly Dictionary<PageName, IView> _views = new();

    public Navigator(IConsole console)
    {
        _console = console;
    }

    public void RegisterView(PageName pageName, IView view)
    {
        _views[pageName] = view;
    }

    public void Run(PageName? startViewName)
    {
        if (!startViewName.HasValue)
        {
            _console.WriteLine("Ошибка: Начальная страница не задана.");
            return;
        }

        PageName? currentViewName = startViewName;
        while (currentViewName.HasValue)
        {
            if (!_views.TryGetValue(currentViewName.Value, out var currentView))
            {
                _console.WriteLine($"Страница '{currentViewName}' не существует. Программа завершена.");
                currentViewName = null;
                continue;
            }

            currentView.Execute();
            currentViewName = currentView.NextViewName;
        }
    }
}