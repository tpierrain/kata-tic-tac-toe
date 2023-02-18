using NFluent;
using NSubstitute;
using NSubstitute.Core;
using System.Numerics;
using TicTacToe.Domain;

namespace TicTacToe.Tests
{
    [TestFixture]
    public class TicTacToeShould
    {
        [Test]
        public void Be_started_before_we_play()
        {
            var messageViewer = Substitute.For<IDisplayMessages>();
            var game = new Game(messageViewer);

            var status = game.Play(1);

            Check.ThatEnum(status).IsEqualTo(Status.NotStarted);
            messageViewer.Received(1).Display("Game not started.");
        }

        [Test]
        public void Display_a_welcome_message_when_starting()
        {
            var messageViewer = Substitute.For<IDisplayMessages>();
            var game = new Game(messageViewer);

            messageViewer.Received(0).Display(Arg.Any<string>());

            game.Start();

            messageViewer.Received(1).Display("New Tic tac toe game started.");
            messageViewer.Received(1).Display("Next player: X");
        }

        [Test]
        public void Ask_X_to_start()
        {
            var messageViewer = Substitute.For<IDisplayMessages>();
            var game = new Game(messageViewer).Start();
            
            messageViewer.Received(1).Display("Next player: X");
        }

        [Test]
        public void Accept_X_first_answer()
        {
            var messageViewer = Substitute.For<IDisplayMessages>();
            var game = new Game(messageViewer).Start();

            var status = game.Play(5);
            Check.ThatEnum(status).IsEqualTo(Status.OnGoing);
        }

        [Test]
        public void Display_X_first_answer()
        {
            var messageViewer = Substitute.For<IDisplayMessages>();
            var game = new Game(messageViewer).Start();

            game.Play(5);
            
            messageViewer.Received(1).Display("X played #5");
        }

        [Test]
        public void Ask_O_to_play_after_X()
        {
            var messageViewer = Substitute.For<IDisplayMessages>();
            var game = new Game(messageViewer).Start();

            messageViewer.Received(0).Display("Next player: O");

            game.Play(5);
            messageViewer.Received(1).Display("Next player: O");
        }

        [Test]
        public void Display_O_first_answer()
        {
            var messageViewer = Substitute.For<IDisplayMessages>();
            var game = new Game(messageViewer).Start();

            game.Play(5);
            messageViewer.Received(1).Display("X played #5");

            game.Play(6);
            messageViewer.Received(1).Display("O played #6");
        }

        [Test]
        public void Tell_the_player_to_try_another_field_if_the_chosen_one_is_already_played()
        {
            var messageViewer = Substitute.For<IDisplayMessages>();
            var game = new Game(messageViewer).Start();

            game.Play(5);
            var status = game.Play(5);

            Check.ThatEnum(status).IsEqualTo(Status.SamePlayerPlayAgain);
            messageViewer.Received(1).Display("#5 is already played. Try another field.");
        }

        [Test]
        public void Only_play_with_valid_field_numbers()
        {
            var messageViewer = Substitute.For<IDisplayMessages>();
            var game = new Game(messageViewer).Start();

            var status = game.Play(fieldNumber: 0);
            Check.ThatEnum(status).IsEqualTo(Status.SamePlayerPlayAgain);
            messageViewer.Received(1).Display("Invalid field number. Please choose a not already played value from 1 to 9");

            for (var i = 1; i <= 9; i++)
            {
                Check.ThatEnum(game.Play(fieldNumber: i)).IsNotEqualTo(Status.SamePlayerPlayAgain);
            }
            messageViewer.Received(1).Display("Invalid field number. Please choose a not already played value from 1 to 9");

            Check.ThatEnum(game.Play(fieldNumber: 10)).IsEqualTo(Status.SamePlayerPlayAgain);
            messageViewer.Received(2).Display("Invalid field number. Please choose a not already played value from 1 to 9");
        }

        [Test]
        public void End_a_game_when_all_fields_in_the_first_column_are_taken_by_player_X()
        {
            var messageViewer = Substitute.For<IDisplayMessages>();
            var game = new Game(messageViewer).Start();

            // X will take: 1, 4, 7

            var status = game.Play(1);
            status = game.Play(2);
            status = game.Play(4);
            status = game.Play(3);
            Check.ThatEnum(status).IsNotEqualTo(Status.Won);

            status = game.Play(7);
            Check.ThatEnum(status).IsEqualTo(Status.Won);
            messageViewer.Received(1).Display("Player X has won the game.");
        }

        [Test]
        public void End_a_game_when_all_fields_in_the_first_column_are_taken_by_player_0()
        {
            var messageViewer = Substitute.For<IDisplayMessages>();
            var game = new Game(messageViewer).Start();

            // 0 will take: 1, 4, 7

            var status = game.Play(2);
            status = game.Play(1);
            status = game.Play(3);
            status = game.Play(4);
            status = game.Play(9);
            Check.ThatEnum(status).IsNotEqualTo(Status.Won);

            status = game.Play(7);
            Check.ThatEnum(status).IsEqualTo(Status.Won);
            messageViewer.Received(1).Display("Player O has won the game.");
        }
    }
}