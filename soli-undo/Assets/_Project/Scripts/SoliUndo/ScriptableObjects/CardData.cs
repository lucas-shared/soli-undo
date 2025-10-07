using SoliUndo.Types;
using UnityEngine;

namespace SoliUndo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
    public class CardData : ScriptableObject
    {
        public int Rank;
        public CardSuit Suit;
        public Sprite Art;
    }
}