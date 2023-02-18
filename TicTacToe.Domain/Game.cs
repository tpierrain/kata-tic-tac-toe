namespace TicTacToe.Domain;

public class Game
{
    private readonly Dictionary<int, Player> _alreadyPlayedFields = new();
    public IDisplayMessages MessageViewer { get; }
    public Player NextPlayer { get; private set; }


    public Game(IDisplayMessages messageViewer)
    {
        NextPlayer = Player.X;
        
        MessageViewer = messageViewer;
        MessageViewer.Display($"Next player: {Enum.GetName(NextPlayer)}");
    }

    public Status Play(int cellNumber)
    {
        if (AlreadyPlayed(cellNumber))
        {
            MessageViewer.Display($"#{cellNumber} is already played. Try another field.");
            
            return Status.SamePlayerPlayAgain;
        }

        MarkFieldAsAlreadyPlayed(cellNumber, NextPlayer);
        MessageViewer.Display($"{Enum.GetName(NextPlayer)} played #{cellNumber}");

        ChangePlayer();

        return Status.OnGoing;
    }

    private void MarkFieldAsAlreadyPlayed(int cellNumber, Player player)
    {
        _alreadyPlayedFields[cellNumber] = player;
    }

    private bool AlreadyPlayed(int cellNumber)
    {
        return _alreadyPlayedFields.ContainsKey(cellNumber);
    }

    private void ChangePlayer()
    {
        NextPlayer = NextPlayer == Player.O ? Player.X : Player.O;
        MessageViewer.Display($"Next player: {Enum.GetName(NextPlayer)}");
    }
}