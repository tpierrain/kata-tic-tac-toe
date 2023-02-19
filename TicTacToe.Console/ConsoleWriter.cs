using TicTacToe.Console;

public class ConsoleWriter : IWriteThings
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}