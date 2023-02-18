namespace TicTacToe.Domain;

public class Game
{
    public IDisplayMessages MessageViewer { get; }

    public Game(IDisplayMessages messageViewer)
    {
        MessageViewer = messageViewer;
        MessageViewer.Display("X plays first");
    }

    public Status Play(Player player, int cellNumber)
    {
        MessageViewer.Display($"{Enum.GetName(player)} played #{cellNumber}");

        return Status.OnGoing;
    }
}