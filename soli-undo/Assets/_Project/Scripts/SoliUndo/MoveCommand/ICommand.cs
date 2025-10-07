
namespace SoliUndo.MoveCommand
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        string Description { get; }
    }
}
