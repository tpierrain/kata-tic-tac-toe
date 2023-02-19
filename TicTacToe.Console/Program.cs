// See https://aka.ms/new-console-template for more information

using TicTacToe.Console;
using TicTacToe.Domain;

var boardDrawer = new AsciiBoardDrawer();
var game = new Game(new ConsoleMessageViewer(), new ConsoleBoardPublisher(boardDrawer))
    .Start();

while (game.Status != GameStatus.Won && game.Status != GameStatus.Draw) 
{
    var input = Console.ReadLine();

    var tryParse = int.TryParse(input, out int fieldNumber);

    game = game.Play(fieldNumber);
}


Console.WriteLine("ciao !");