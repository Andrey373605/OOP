using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Views;

namespace OOP_LAB1.Presentation.Registration
{
    public static class ViewRegistrar
    {
        public static void RegisterViews(IServiceProvider serviceProvider, INavigator navigator)
        {
            // Поиск всех типов, реализующих IView
            var viewTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(IView).IsAssignableFrom(t) 
                            && !t.IsInterface 
                            && !t.IsAbstract)
                .ToList();

            System.Console.WriteLine(viewTypes.Count());
            
            foreach (var viewType in viewTypes)
            {
                // Находим атрибут ViewMapping на классе
                var attribute = viewType.GetCustomAttribute<ViewMappingAttribute>();
                System.Console.WriteLine($"{attribute.PageName}");
                
                
                if (attribute != null)
                {
                    // Создаем экземпляр view через DI
                    var view = (IView)serviceProvider.GetService(viewType);
                    
                    
                        
                    
                    if (view != null)
                    {
                        // Регистрируем view в навигаторе
                        navigator.RegisterView(attribute.PageName, view);
                    }
                }
            }
        }
    }
}