namespace OOP_LAB1.Presentation.Console;

public interface IConsoleView
{
    void WriteLine(string message);
    string ReadLine();
    void Clear();
}