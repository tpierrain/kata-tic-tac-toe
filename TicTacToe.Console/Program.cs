// See https://aka.ms/new-console-template for more information

using TicTacToe.Console;
using TicTacToe.Domain;

var game = new Game(new ConsoleMessageViewer());

var input = Console.ReadLine();

var tryParse = int.TryParse(input, out int fieldName);

var status = game.Play(fieldName);

Console.WriteLine("ciao !");
