using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.OperatorApproveSalaryProjectPage)]
public class OperatorApproveSalaryProjectView : IView
{
    public PageName? NextViewName { get; }
    public Task Execute()
    {
        throw new NotImplementedException();
    }
}