using OOP_LAB1.Presentation.Enums;

namespace OOP_LAB1.Presentation.Navigator;

public class ViewMappingAttribute : Attribute
{
    public PageName PageName { get; }

    public ViewMappingAttribute(PageName pageName)
    {
        PageName = pageName;
    }
}