using TicTacToe.Domain;

namespace TicTacToe.Console;

public class ConsoleMessageViewer : IDisplayMessages
{
    public void Display(string instruction)
    {
        System.Console.WriteLine(instruction);
    }
}