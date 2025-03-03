using OOP_LAB1.Presentation.Views;
using OOP_LAB1.Presentation.Enums;
namespace OOP_LAB1.Presentation.Navigator;

public interface INavigator
{
    public void RegisterView(PageName pageName, IView view);

    public void Run(PageName? startViewName);

}