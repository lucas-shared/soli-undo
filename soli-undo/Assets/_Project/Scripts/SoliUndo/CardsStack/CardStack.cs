using System.Collections.Generic;
using UnityEngine;

namespace SoliUndo.CardsStack
{
    public class CardStack : MonoBehaviour
    {
        [Header("Stack Configuration")]
        [SerializeField] private CardStackType stackType;
        [SerializeField] private Transform cardContainer;
        [SerializeField] private float cardSpacing = -0.2f;
        [SerializeField] private float zOffset = -0.01f;
        
        public CardStackType StackType => stackType;
        private readonly List<Card> _cards = new();
        

        public void AddCard(Card card)
        {
            if (card == null) return;
        
            _cards.Add(card);
            card.transform.SetParent(cardContainer);
        
            var position = cardContainer.position;
            position.y += _cards.Count * cardSpacing;
            position.z = cardContainer.position.z + (_cards.Count * zOffset);
            card.transform.position = position;
        
            card.SetCurrentStack(this);
            UpdateCardFaces();
        }
        
        public Card RemoveCard(Card card)
        {
            if (card == null || !_cards.Contains(card)) return null;
        
            var index = _cards.IndexOf(card);
            _cards.RemoveAt(index);
        
            for (var i = index; i < _cards.Count; i++)
            {
                var position = cardContainer.position;
                position.y += (i + 1) * cardSpacing;
                position.z = cardContainer.position.z + ((i + 1) * zOffset);
                _cards[i].transform.position = position;
            }
        
            UpdateCardFaces();
        
            return card;
        }
        
        private void UpdateCardFaces()
        {
            for (var i = 0; i < _cards.Count; i++)
            {
                var shouldBeFaceUp = (i == _cards.Count - 1);
                _cards[i].SetFaceUp(shouldBeFaceUp);
            }
        }
        
        public bool CanPlaceCard(Card card)
        {
            if (card == null) return false;

            return stackType switch
            {
                CardStackType.Foundation => true,
                CardStackType.Tableau => true,
                CardStackType.Waste => true,
                _ => false
            };
        }
    }
}
