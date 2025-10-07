using UnityEngine;

namespace SoliUndo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoliUndoConfig", menuName = "Scriptable Objects/SoliUndoConfig")]
    public class SoliUndoConfig : ScriptableObject
    {
        [Header("Undo Configuration")]
        [SerializeField] private int maxUndoSteps = 10;
    
        [Header("Cards Data")]
        [SerializeField] private AllCardsData cardsData;
    
        public AllCardsData CardsData => cardsData;
        public int MaxUndoSteps => maxUndoSteps;
    }
}
