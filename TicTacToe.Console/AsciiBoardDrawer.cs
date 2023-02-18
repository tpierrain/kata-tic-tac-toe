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