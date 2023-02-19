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

        _console.Write($"+-");
        _console.Write(field1);
        _console.Write("-+-");
        _console.Write(field2);
        _console.Write("-+-");
        _console.Write(field3);
        _console.Write("-+.");
        _console.Write(Environment.NewLine);
        
        _console.WriteLine($"+---+---+---+");

        _console.Write($"+-");
        _console.Write(field4);
        _console.Write("-+-");
        _console.Write(field5);
        _console.Write("-+-");
        _console.Write(field6);
        _console.Write("-+.");
        _console.Write(Environment.NewLine);
        
        _console.WriteLine($"+---+---+---+");

        _console.Write($"+-");
        _console.Write(field7);
        _console.Write("-+-");
        _console.Write(field8);
        _console.Write("-+-");
        _console.Write(field9);
        _console.Write("-+.");
        _console.Write(Environment.NewLine);

        _console.WriteLine($"+---+---+---+");
        _console.WriteLine($"");
    }
    public void Draw(string[] board)
    {
        Draw(board[0], board[1], board[2], board[3], board[4], board[5], board[6], board[7], board[8]);
    }
}