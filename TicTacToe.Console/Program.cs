// See https://aka.ms/new-console-template for more information

using TicTacToe.Console;
using TicTacToe.Domain;

var game = new Game(new ConsoleMessageViewer()).Start();
var boardDrawer = new BoardDrawer();

while (game.Status != GameStatus.Won && game.Status != GameStatus.Draw) 
{
    var input = Console.ReadLine();

    var tryParse = int.TryParse(input, out int fieldNumber);

    game = game.Play(fieldNumber);

    boardDrawer.Draw(game.Board);
}


Console.WriteLine("ciao !");

public class BoardDrawer
{
    private readonly IWriteThings _console;

    public BoardDrawer() : this(new ConsoleWriter())
    {
    }

    public BoardDrawer(IWriteThings console)
    {
        _console = console;
    }

    public void Draw(string[] board)
    {
        _console.WriteLine($"+---+---+---+");
        _console.WriteLine($"+-{board[0]}-+-{board[1]}-+-{board[2]}-+");
        _console.WriteLine($"+---+---+---+");
        _console.WriteLine($"+-{board[3]}-+-{board[4]}-+-{board[5]}-+");
        _console.WriteLine($"+---+---+---+");
        _console.WriteLine($"+-{board[6]}-+-{board[7]}-+-{board[8]}-+");
        _console.WriteLine($"+---+---+---+");
    }
}

public class ConsoleWriter : IWriteThings
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}
