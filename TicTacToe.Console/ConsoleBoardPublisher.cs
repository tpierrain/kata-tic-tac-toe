using TicTacToe.Domain;

namespace TicTacToe.Console;

public class ConsoleBoardPublisher : IPublishBoards
{
    private readonly AsciiBoardDrawer _boardDrawer;

    public ConsoleBoardPublisher(AsciiBoardDrawer boardDrawer)
    {
        _boardDrawer = boardDrawer;
    }

    public void Publish(string field1, string field2, string field3, string field4, string field5, string field6, string field7,
        string field8, string field9)
    {
        _boardDrawer.Draw(field1, field2, field3, field4, field5, field6, field7, field8, field9);
    }
}