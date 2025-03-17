namespace OOP_LAB1.Presentation.Console;

public interface IConsole
{
    void WriteLine(string message);
    string ReadLine();
    void Clear();
    
    void WriteMenuList(List<string> menuList);
}