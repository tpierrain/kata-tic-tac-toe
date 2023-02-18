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
        }
    }
}