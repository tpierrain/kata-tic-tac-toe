namespace TicTacToe.Domain;

public class Game
{
    public IDisplayMessages MessageViewer { get; }
    public Player NextPlayer { get; private set; }


    public Game(IDisplayMessages messageViewer)
    {
        MessageViewer = messageViewer;
        NextPlayer = Player.X;
        MessageViewer.Display($"Next player: {Enum.GetName(NextPlayer)}");
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
        NextPlayer = NextPlayer == Player.O ? Player.X : Player.O;
    }
}