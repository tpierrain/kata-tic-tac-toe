namespace TicTacToe.Domain;

public class Game
{
    private bool _started;
    
    private readonly HashSet<int> _alreadyPlayedFieldsByX = new();
    private readonly HashSet<int> _alreadyPlayedFieldsByO = new();

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

        if (HasWon(CurrentPlayer))
        {
            MessageViewer.Display($"Player {Enum.GetName(CurrentPlayer)} has won the game.");
            return Status.Won;
        }

        SwitchPlayer();

        return Status.OnGoing;
    }

    private bool HasWon(Player player)
    {
        if (_alreadyPlayedFieldsByX.Contains(1) 
            && _alreadyPlayedFieldsByX.Contains(4) &&
            _alreadyPlayedFieldsByX.Contains(7))
        {
            return true;
        }

        return false;
    }

    private static bool IsInvalidFieldNumber(int fieldNumber)
    {
        return fieldNumber is < 1 or > 9;
    }

    private void MarkFieldAsAlreadyPlayed(int fieldNumber, Player player)
    {
        if (player == Player.X)
        {
            _alreadyPlayedFieldsByX.Add(fieldNumber);
        }
        else
        {
            _alreadyPlayedFieldsByO.Add(fieldNumber);
        }

        MessageViewer.Display($"{Enum.GetName(CurrentPlayer)} played #{fieldNumber}");
    }

    private bool IsAlreadyPlayed(int fieldNumber)
    {
        return _alreadyPlayedFieldsByX.Contains(fieldNumber) || _alreadyPlayedFieldsByO.Contains(fieldNumber);
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