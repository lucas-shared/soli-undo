using System.Collections.Generic;
using SoliUndo.ScriptableObjects;
using UnityEngine;

namespace SoliUndo
{
    public class CardsInitializer
    {
        public List<Card> CreateCards(GameObject cardPrefab, AllCardsData cardsData)
        {
            var cards = new List<Card>();

            var cardBackArt = cardsData.CardBackArt;
        
            foreach (var cardData in cardsData.AllCards)
            {
                var cardObject = GameObject.Instantiate(cardPrefab);
                var card = cardObject.GetComponent<Card>();
                card.Initialize(cardData, cardBackArt);
                cards.Add(card);
            }

            return cards;
        }
    }
}