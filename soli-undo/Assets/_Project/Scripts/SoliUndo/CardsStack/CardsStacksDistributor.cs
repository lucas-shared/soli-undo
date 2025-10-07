using System.Collections.Generic;
using SoliUndo;
using SoliUndo.CardsStack;

namespace SoliUndo.CardsStack
{
    public class CardsStacksDistributor
    {
        public void DealCards(List<Card> allCards, List<CardStack> cardStacks)
        {
            var cardsPerStack = allCards.Count / cardStacks.Count;
            var cardIndex = 0;
        
            foreach (var stack in cardStacks)
            {
                for (var i = 0; i < cardsPerStack && cardIndex < allCards.Count; i++)
                {
                    var card = allCards[cardIndex];
                    stack.AddCard(card);
                    cardIndex++;
                }
            }
        }
    }
}