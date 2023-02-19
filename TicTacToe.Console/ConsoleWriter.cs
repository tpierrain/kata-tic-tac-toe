using System.Drawing;
using TicTacToe.Console;

public class ConsoleWriter : IWriteThings
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void Write(string message)
    {
        Console.Write(message);
    }
}