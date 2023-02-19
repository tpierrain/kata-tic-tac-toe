using TicTacToe.Console;

public class AsciiBoardDrawer
{
    private readonly IWriteThings _console;

    public AsciiBoardDrawer() : this(new ConsoleWriter())
    {
    }

    public AsciiBoardDrawer(IWriteThings console)
    {
        _console = console;
    }

    public void Draw(string field1, string field2, string field3, string field4, string field5, string field6,
        string field7, string field8, string field9)
    {
        _console.WriteLine($"");
        _console.WriteLine($"+---+---+---+");
        _console.WriteLine($"+-{field1}-+-{field2}-+-{field3}-+");
        _console.WriteLine($"+---+---+---+");
        _console.WriteLine($"+-{field4}-+-{field5}-+-{field6}-+");
        _console.WriteLine($"+---+---+---+");
        _console.WriteLine($"+-{field7}-+-{field8}-+-{field9}-+");
        _console.WriteLine($"+---+---+---+");
        _console.WriteLine($"");
    }
    public void Draw(string[] board)
    {
        _console.WriteLine($"");
        _console.WriteLine($"+---+---+---+");
        _console.WriteLine($"+-{board[0]}-+-{board[1]}-+-{board[2]}-+");
        _console.WriteLine($"+---+---+---+");
        _console.WriteLine($"+-{board[3]}-+-{board[4]}-+-{board[5]}-+");
        _console.WriteLine($"+---+---+---+");
        _console.WriteLine($"+-{board[6]}-+-{board[7]}-+-{board[8]}-+");
        _console.WriteLine($"+---+---+---+");
        _console.WriteLine($"");
    }
}