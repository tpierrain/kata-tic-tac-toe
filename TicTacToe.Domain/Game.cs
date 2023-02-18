namespace TicTacToe.Domain;

public class Game
{
    public IDisplayMessages MessageViewer { get; }
    public Player NextPlayer { get; private set; }


    public Game(IDisplayMessages messageViewer)
    {
        MessageViewer = messageViewer;
        MessageViewer.Display("X plays first");
        NextPlayer = Player.X;
    }

    public Status Play(int cellNumber)
    {
        MessageViewer.Display($"{Enum.GetName(NextPlayer)} played #{cellNumber}");

        ChangePlayer();

        MessageViewer.Display($"Next player: {Enum.GetName(NextPlayer)}");

        return Status.OnGoing;
    }

    private void ChangePlayer()
    {
        if (NextPlayer == Player.O)
        {
            NextPlayer = Player.X;
        }
        else
        {
            NextPlayer = Player.O;
        }
    }
}