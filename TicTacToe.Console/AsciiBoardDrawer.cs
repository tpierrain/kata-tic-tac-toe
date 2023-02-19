using TicTacToe.Console;

public class AsciiBoardDrawer
{
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
        WriteNewLine();
        
        WriteSeparatorLine();
        WriteLineWithFields(field1, field2, field3);
        WriteSeparatorLine();
        WriteLineWithFields(field4, field5, field6);
        WriteSeparatorLine();
        WriteLineWithFields(field7, field8, field9);
        WriteSeparatorLine();

        WriteNewLine();
    }

    private void WriteNewLine()
    {
        _console.WriteLine(string.Empty);
    }

    private void WriteSeparatorLine()
    {
        _console.WriteLine($"+---+---+---+");
    }

    private void WriteLineWithFields(string field1, string field2, string field3)
    {
        _console.Write($"+-");
        _console.Write(field1);
        _console.Write("-+-");
        _console.Write(field2);
        _console.Write("-+-");
        _console.Write(field3);
        _console.Write("-+.");
        _console.Write(Environment.NewLine);
    }

    public void Draw(string[] board)
    {
        Draw(board[0], board[1], board[2], board[3], board[4], board[5], board[6], board[7], board[8]);
    }
}