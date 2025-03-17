namespace OOP_LAB1.Presentation.Console;

public class Console : IConsole
{
    public void WriteLine(string message) => System.Console.WriteLine(message);
    public string ReadLine() => System.Console.ReadLine();
    public void Clear() => System.Console.Clear();
    public void WriteMenuList(List<string> menuList)
    {
        foreach (var (item, index) in menuList.Select((value, i) => (value, i)))
        {
            WriteLine($"{index}. {item}");
        }
    }
}