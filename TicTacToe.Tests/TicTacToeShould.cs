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
            var displayDevice = Substitute.For<IDisplayMessages>();
            var game = new Game(displayDevice);
            
            displayDevice.Received(1).Display("X plays first");
        }

        [Test]
        public void Accept_X_First_Answer()
        {
            var displayDevice = Substitute.For<IDisplayMessages>();
            var game = new Game(displayDevice);

            var status = game.Play(Player.X, 5);
            Check.ThatEnum(status).IsEqualTo(Status.OnGoing);
        }

        [Test]
        public void Display_X_First_Answer()
        {
            var displayDevice = Substitute.For<IDisplayMessages>();
            var game = new Game(displayDevice);

            game.Play(Player.X, 5);
            
            displayDevice.Received(1).Display("X played #5");
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

    public interface IDisplayMessages
    {
        void Display(string instruction);
    }

    public class Game
    {
        public IDisplayMessages DisplayDevice { get; }

        public Game(IDisplayMessages displayDevice)
        {
            DisplayDevice = displayDevice;
            DisplayDevice.Display("X plays first");
        }

        public Status Play(Player player, int cellNumber)
        {
            DisplayDevice.Display($"{Enum.GetName(player)} played #{cellNumber}");

            return Status.OnGoing;
        }
    }
}