using TicTacToe.Console;

public class AsciiBoardDrawer
{
    private const ConsoleColor ForegroundColorForFrame = ConsoleColor.DarkGray;
    private const ConsoleColor ForegroundColorForX = ConsoleColor.Yellow;
    private const ConsoleColor ForegroundColorForO = ConsoleColor.Red;

    private readonly IWriteThings _console;

    public AsciiBoardDrawer() : this(new ConsoleWriter())
    {
    }

    public AsciiBoardDrawer(IWriteThings console)
    {
        _console = console;
    }

    public void Draw(string field1, string field2, string field3, string field4, string field5, string field6,
        string field7, string field8, string field9)
    {
        var previousForegroundColor = Console.ForegroundColor;

        WriteNewLine();
        
        WriteSeparatorLine();
        WriteLineWithFields(field1, field2, field3);
        WriteSeparatorLine();
        WriteLineWithFields(field4, field5, field6);
        WriteSeparatorLine();
        WriteLineWithFields(field7, field8, field9);
        WriteSeparatorLine();

        WriteNewLine();

        Console.ForegroundColor = previousForegroundColor;
    }

    private void WriteNewLine()
    {
        _console.WriteLine(string.Empty);
    }

    private void WriteSeparatorLine()
    {
        Console.ForegroundColor = ForegroundColorForFrame;
        _console.WriteLine($"+---+---+---+");
    }

    private void WriteLineWithFields(string fieldA, string fieldB, string fieldC)
    {
        Console.ForegroundColor = ForegroundColorForFrame;

        WriteInTheProperFrameColor($"+-");
        WriteFieldInTheProperColor(fieldA);
        WriteInTheProperFrameColor("-+-");
        WriteFieldInTheProperColor(fieldB);
        WriteInTheProperFrameColor("-+-");
        WriteFieldInTheProperColor(fieldC);
        WriteInTheProperFrameColor("-+.");

        _console.Write(Environment.NewLine);
    }

    private void WriteInTheProperFrameColor(string message)
    {
        Console.ForegroundColor = ForegroundColorForFrame;
        _console.Write(message);
    }

    private void WriteFieldInTheProperColor(string field)
    {
        if (field == "X")
        {
            Console.ForegroundColor = ForegroundColorForX;
        }
        else if (field == "O")
        {
            Console.ForegroundColor = ForegroundColorForO;
        }
        else
        {
            Console.ForegroundColor = ForegroundColorForFrame;

        }

        _console.Write(field);
    }

    public void Draw(string[] board)
    {
        Draw(board[0], board[1], board[2], board[3], board[4], board[5], board[6], board[7], board[8]);
    }
}