using System.Collections.Generic;
using UnityEngine;

namespace SoliUndo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AllCardsData", menuName = "Scriptable Objects/AllCardsData")]
    public class AllCardsData : ScriptableObject
    {
        [SerializeField] private List<CardData> allCards;
        [SerializeField] Sprite cardBackArt;
    
        public List<CardData> AllCards => allCards;
        public Sprite CardBackArt => cardBackArt;
    }
}
