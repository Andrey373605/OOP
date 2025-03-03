using OOP_LAB1.Presentation.Enums;

namespace OOP_LAB1.Presentation.Views;

public interface IView
{
    public PageName? NextViewName { get; }
    void Execute();
}