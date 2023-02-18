namespace TicTacToe.Domain;

public class Game
{
    private readonly Dictionary<int, Player> _alreadyPlayedFields = new();
    private bool _started;
    public IDisplayMessages MessageViewer { get; }
    public Player CurrentPlayer { get; private set; }
    
    public Game(IDisplayMessages messageViewer)
    {
        CurrentPlayer = Player.X;
        
        MessageViewer = messageViewer;
    }

    public Status Play(int fieldNumber)
    {
        if (!_started)
        {
            MessageViewer.Display("Game not started.");
            return Status.NotStarted;
        }

        if(IsInvalidFieldNumber(fieldNumber))
        {
            MessageViewer.Display("Invalid field number. Please choose a not already played value from 1 to 9");
            return Status.SamePlayerPlayAgain;
        }

        if (IsAlreadyPlayed(fieldNumber))
        {
            MessageViewer.Display($"#{fieldNumber} is already played. Try another field.");
            
            return Status.SamePlayerPlayAgain;
        }

        MarkFieldAsAlreadyPlayed(fieldNumber, CurrentPlayer);

        SwitchPlayer();

        return Status.OnGoing;
    }

    private static bool IsInvalidFieldNumber(int fieldNumber)
    {
        return fieldNumber is < 1 or > 9;
    }

    private void MarkFieldAsAlreadyPlayed(int fieldNumber, Player player)
    {
        _alreadyPlayedFields[fieldNumber] = player;
        MessageViewer.Display($"{Enum.GetName(CurrentPlayer)} played #{fieldNumber}");
    }

    private bool IsAlreadyPlayed(int fieldNumber)
    {
        return _alreadyPlayedFields.ContainsKey(fieldNumber);
    }

    private void SwitchPlayer()
    {
        CurrentPlayer = CurrentPlayer == Player.O ? Player.X : Player.O;
        MessageViewer.Display($"Next player: {Enum.GetName(CurrentPlayer)}");
    }

    public Game Start()
    {
        _started = true;
        MessageViewer.Display("New Tic tac toe game started.");
        MessageViewer.Display($"Next player: {Enum.GetName(CurrentPlayer)}");

        return this;
    }
}