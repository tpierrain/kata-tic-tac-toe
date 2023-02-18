namespace TicTacToe.Domain;

public class Game
{
    private bool _started;
   
    private readonly Dictionary<Player, HashSet<int>> _alreadyPlayedFields = new();
    
    public IDisplayMessages MessageViewer { get; }
    public Player CurrentPlayer { get; private set; }
    
    public Game(IDisplayMessages messageViewer)
    {
        _alreadyPlayedFields[Player.X] = new HashSet<int>();
        _alreadyPlayedFields[Player.O] = new HashSet<int>();

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

        if (_alreadyPlayedFields.Sum(l => l.Value.Count) == 9)
        {
            MessageViewer.Display("Game ended on a Draw.");
            return Status.Draw;
        }

        SwitchPlayer();

        return Status.OnGoing;
    }

    private bool HasWon(Player player)
    {
        if (HasTakenTheFirstColumn(player) || HasTakenTheSecondColumn(player) || HasTakenTheThirdColumn(player))
        {
            return true;
        }

        if (HasTakenTheFirstRow(player) || HasTakenTheSecondRow(player) || HasTakenTheThirdRow(player))
        {
            return true;
        }

        if (HasTakenADiagonal(player))
        {
            return true;
        }

        return false;
    }

    private bool HasTakenADiagonal(Player player)
    {
        return (_alreadyPlayedFields[player].Contains(1)
               && _alreadyPlayedFields[player].Contains(5)
               && _alreadyPlayedFields[player].Contains(9))
               || 
               (_alreadyPlayedFields[player].Contains(3)
               && _alreadyPlayedFields[player].Contains(5)
               && _alreadyPlayedFields[player].Contains(7));
    }

    private bool HasTakenTheFirstColumn(Player player)
    {
        return _alreadyPlayedFields[player].Contains(1) 
               && _alreadyPlayedFields[player].Contains(4) 
               && _alreadyPlayedFields[player].Contains(7);
    }

    private bool HasTakenTheSecondColumn(Player player)
    {
        return _alreadyPlayedFields[player].Contains(2)
               && _alreadyPlayedFields[player].Contains(5)
               && _alreadyPlayedFields[player].Contains(8);
    }

    private bool HasTakenTheThirdColumn(Player player)
    {
        return _alreadyPlayedFields[player].Contains(3)
               && _alreadyPlayedFields[player].Contains(6)
               && _alreadyPlayedFields[player].Contains(9);
    }

    private bool HasTakenTheFirstRow(Player player)
    {
        return _alreadyPlayedFields[player].Contains(1)
               && _alreadyPlayedFields[player].Contains(2)
               && _alreadyPlayedFields[player].Contains(3);
    }

    private bool HasTakenTheSecondRow(Player player)
    {
        return _alreadyPlayedFields[player].Contains(4)
               && _alreadyPlayedFields[player].Contains(5)
               && _alreadyPlayedFields[player].Contains(6);
    }

    private bool HasTakenTheThirdRow(Player player)
    {
        return _alreadyPlayedFields[player].Contains(7)
               && _alreadyPlayedFields[player].Contains(8)
               && _alreadyPlayedFields[player].Contains(9);
    }

    private static bool IsInvalidFieldNumber(int fieldNumber)
    {
        return fieldNumber is < 1 or > 9;
    }

    private void MarkFieldAsAlreadyPlayed(int fieldNumber, Player player)
    {
        _alreadyPlayedFields[player].Add(fieldNumber);

        MessageViewer.Display($"{Enum.GetName(CurrentPlayer)} played #{fieldNumber}");
    }

    private bool IsAlreadyPlayed(int fieldNumber)
    {
        return _alreadyPlayedFields[Player.X].Contains(fieldNumber) || _alreadyPlayedFields[Player.O].Contains(fieldNumber);
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