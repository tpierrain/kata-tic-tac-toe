// See https://aka.ms/new-console-template for more information

using TicTacToe.Console;
using TicTacToe.Domain;

var game = new Game(new ConsoleMessageViewer()).Start();

var status = GameStatus.OnGoing;

while (status != GameStatus.Won && status != GameStatus.Draw) 
{
    var input = Console.ReadLine();

    var tryParse = int.TryParse(input, out int fieldNumber);

    status = game.Play(fieldNumber);
}


Console.WriteLine("ciao !");
