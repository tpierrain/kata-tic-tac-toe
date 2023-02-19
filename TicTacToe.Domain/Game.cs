namespace TicTacToe.Domain;

public class Game
{
    private readonly IPublishBoards _boardPublisher;
    private const int NumberOfRows = 3;
    private const int NumberOfColumns = 3;

    private const int MaxNumberOfElements = NumberOfRows * NumberOfColumns;

    private bool _started;
   
    private readonly Dictionary<Player, HashSet<int>> _alreadyPlayedFields = new();
    
    public IDisplayMessages MessageViewer { get; }
    public Player CurrentPlayer { get; private set; }

    public GameStatus Status { get; private set; }
    public string[] Board => ExportBoard();

    private string[] ExportBoard()
    {
        return Enumerable.Range(1, MaxNumberOfElements).Select(x =>
        {
            if (_alreadyPlayedFields[Player.X].Contains(x))
            {
                return "X";
            }
            else if (_alreadyPlayedFields[Player.O].Contains(x))
            {
                return "O";
            }
            return x.ToString();
        }).ToArray();
    }

    public Game(IDisplayMessages messageViewer) : this(messageViewer, new NullBoardPublisher())
    {
    }

    public Game(IDisplayMessages messageViewer, IPublishBoards boardPublisher = null)
    {
        _boardPublisher = boardPublisher;
        Status = GameStatus.OnGoing;
        _alreadyPlayedFields[Player.X] = new HashSet<int>();
        _alreadyPlayedFields[Player.O] = new HashSet<int>();

        CurrentPlayer = Player.X;
        
        MessageViewer = messageViewer;
    }

    public Game Play(int fieldNumber)
    {
        if (!_started)
        {
            MessageViewer.Display("Game not started.");
            Status = GameStatus.NotStarted;
            return this;
        }

        if(IsInvalidFieldNumber(fieldNumber))
        {
            PublishBoard();

            MessageViewer.Display("Invalid field number. Please choose a not already played value from 1 to 9");
            Status = GameStatus.SamePlayerPlayAgain;
            return this;
        }

        if (IsAlreadyPlayed(fieldNumber))
        {
            PublishBoard();

            MessageViewer.Display($"#{fieldNumber} is already played. Try another field.");
            Status = GameStatus.SamePlayerPlayAgain;
            return this;
        }

        MarkFieldAsAlreadyPlayed(fieldNumber, CurrentPlayer);

        if (HasWon(CurrentPlayer))
        {
            MessageViewer.Display($"Player {Enum.GetName(CurrentPlayer)} has won the game.");
            Status = GameStatus.Won;
            return this;
        }

        if (SumOfAlreadyPlayedFields() == MaxNumberOfElements)
        {
            MessageViewer.Display("Game ended on a Draw.");
            Status = GameStatus.Draw;
            return this;
        }

        SwitchPlayer();
        PublishBoard();
        MessageViewer.Display($"Next player: {Enum.GetName(CurrentPlayer)}");

        Status = GameStatus.OnGoing;
        return this;
    }

    private void PublishBoard()
    {
        var fields = this.Board.ToArray();
        _boardPublisher.Publish(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6], fields[7], fields[8]);
    }

    private int SumOfAlreadyPlayedFields()
    {
        return _alreadyPlayedFields.Sum(l => l.Value.Count);
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
    }

    public Game Start()
    {
        _started = true;
        MessageViewer.Display("New Tic tac toe game started.");
        PublishBoard();
        MessageViewer.Display($"Next player: {Enum.GetName(CurrentPlayer)}");

        return this;
    }
}