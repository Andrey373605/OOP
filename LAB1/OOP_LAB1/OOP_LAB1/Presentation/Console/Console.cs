namespace OOP_LAB1.Presentation.Console;

public class Console : IConsole
{
    public void WriteLine(string message) => System.Console.WriteLine(message);
    public string ReadLine() => System.Console.ReadLine();
    public void Clear() => System.Console.Clear();

}