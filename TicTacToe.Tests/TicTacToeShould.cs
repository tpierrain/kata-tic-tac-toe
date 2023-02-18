using NFluent;
using NSubstitute;
using TicTacToe.Domain;

namespace TicTacToe.Tests
{
    [TestFixture]
    public class TicTacToeShould
    {
        [Test]
        public void Ask_X_to_start()
        {
            var messageViewer = Substitute.For<IDisplayMessages>();
            var game = new Game(messageViewer);
            
            messageViewer.Received(1).Display("X plays first");
        }

        [Test]
        public void Accept_X_First_Answer()
        {
            var messageViewer = Substitute.For<IDisplayMessages>();
            var game = new Game(messageViewer);

            var status = game.Play(5);
            Check.ThatEnum(status).IsEqualTo(Status.OnGoing);
        }

        [Test]
        public void Display_X_First_Answer()
        {
            var messageViewer = Substitute.For<IDisplayMessages>();
            var game = new Game(messageViewer);

            game.Play(5);
            
            messageViewer.Received(1).Display("X played #5");
        }
    }
}