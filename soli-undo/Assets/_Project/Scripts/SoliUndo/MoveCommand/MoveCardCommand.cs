using SoliUndo.CardsStack;
using UnityEngine;

namespace SoliUndo.MoveCommand
{
    public class MoveCardCommand : ICommand
    {
        private Card card;
        private CardStack fromStack;
        private CardStack toStack;
        private Vector3 originalPosition;
    
        public MoveCardCommand(Card card, CardStack fromStack, CardStack toStack)
        {
            this.card = card;
            this.fromStack = fromStack;
            this.toStack = toStack;
            originalPosition = card.OriginalPosition;
        }
    
        public void Execute()
        {
            fromStack.RemoveCard(card);
            toStack.AddCard(card);
            card.SetCurrentStack(toStack);
        }
    
        public void Undo()
        {
            toStack.RemoveCard(card);
            fromStack.AddCard(card);
            card.SetCurrentStack(fromStack);
            card.transform.position = originalPosition;
        }
    
        public string Description => $"Move {card.CardData.Rank} of {card.CardData.Suit} from {fromStack.StackType} to {toStack.StackType}";
    
    }
}
