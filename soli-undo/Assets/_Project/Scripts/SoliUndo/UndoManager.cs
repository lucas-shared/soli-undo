using System.Collections.Generic;
using SoliUndo.MoveCommand;
using UnityEngine;

namespace SoliUndo
{
    public class UndoManager
    {
        private int _maxUndoSteps = 10;
        private readonly Stack<ICommand> _undoStack = new();
        
        public UndoManager(int maxUndoSteps)
        {
            _maxUndoSteps = maxUndoSteps;
        }
        
        public void ExecuteCommand(ICommand command)
        {
            if (command == null) return;
        
            command.Execute();
            _undoStack.Push(command);
        
            if (_undoStack.Count > _maxUndoSteps)
            {
                var tempStack = new Stack<ICommand>();
                for (var i = 0; i < _maxUndoSteps - 1; i++)
                {
                    tempStack.Push(_undoStack.Pop());
                }
                _undoStack.Clear();
                while (tempStack.Count > 0)
                {
                    _undoStack.Push(tempStack.Pop());
                }
            }
        
            Debug.Log($"Command executed: {command.Description}");
        }

        public bool UndoLastCommand()
        {
            if (_undoStack.Count == 0)
            {
                Debug.Log("No commands to undo");
                return false;
            }
        
            var lastCommand = _undoStack.Pop();
            lastCommand.Undo();
        
            Debug.Log($"Command undone: {lastCommand.Description}");
            return true;
        }
        
        public bool CanUndo => _undoStack.Count > 0;
        
        public void ClearUndoHistory()
        {
            _undoStack.Clear();
        }
    }
}
