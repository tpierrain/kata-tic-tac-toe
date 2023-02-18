using NSubstitute;
using TicTacToe.Console;
using TicTacToe.Domain;

namespace TicTacToe.Tests;

[TestFixture]
public class BoardDrawerShould
{
    [Test]
    public void Sketch_an_Empty_board()
    {
        var console = Substitute.For<IWriteThings>();
        var boardDrawer = new BoardDrawer(console);
        var game = new Game(Substitute.For<IDisplayMessages>());
        var emptyBoard = game.Board;

        boardDrawer.Draw(emptyBoard);

        console.Received().WriteLine("+---+---+---+");
        console.Received().WriteLine("+-1-+-2-+-3-+");
        console.Received().WriteLine("+---+---+---+");
        console.Received().WriteLine("+-4-+-5-+-6-+");
        console.Received().WriteLine("+---+---+---+");
        console.Received().WriteLine("+-7-+-8-+-9-+");
    }

    [Test]
    public void Sketch_a_board_with_X_and_O_moves()
    {
        var console = Substitute.For<IWriteThings>();
        var boardDrawer = new BoardDrawer(console);
        
        var game = new Game(Substitute.For<IDisplayMessages>()).Start();
        game = game.Play(5); // X
        game = game.Play(8); // O
        game = game.Play(3); // X
        game = game.Play(7); // O

        boardDrawer.Draw(game.Board);

        console.Received().WriteLine("+---+---+---+");
        console.Received().WriteLine("+-1-+-2-+-X-+");
        console.Received().WriteLine("+---+---+---+");
        console.Received().WriteLine("+-4-+-X-+-6-+");
        console.Received().WriteLine("+---+---+---+");
        console.Received().WriteLine("+-O-+-O-+-9-+");
    }
}