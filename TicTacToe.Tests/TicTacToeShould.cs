using NFluent;
using NSubstitute;

namespace TicTacToe.Tests
{
    [TestFixture]
    public class TicTacToeShould
    {
        [Test]
        public void Ask_X_to_start()
        {
            var instructor = Substitute.For<IDisplayInstructions>();
            var game = new Game(instructor);
            
            instructor.Received(1).Display("X plays first");
        }

        [Test]
        public void Accept_X_First_Answer()
        {
            var instructor = Substitute.For<IDisplayInstructions>();
            var game = new Game(instructor);

            var status = game.Play(Player.X, 5);
            Check.ThatEnum(status).IsEqualTo(Status.OnGoing);
        }

        [Test]
        public void Display_X_First_Answer()
        {
            var instructor = Substitute.For<IDisplayInstructions>();
            var game = new Game(instructor);

            var status = game.Play(Player.X, 5);
            
            instructor.Received(1).Display("X played #5");
        }
    }

    public enum Status
    {
        OnGoing
    }

    public enum Player
    {
        X,
        O
    }

    public interface IDisplayInstructions
    {
        void Display(string instruction);
    }

    public class Game
    {
        public IDisplayInstructions Instructor { get; }

        public Game(IDisplayInstructions instructor)
        {
            Instructor = instructor;
            Instructor.Display("X plays first");
        }

        public Status Play(Player player, int cellNumber)
        {
            return Status.OnGoing;
        }
    }
}