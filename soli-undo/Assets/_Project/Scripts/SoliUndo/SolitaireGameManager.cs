using System.Collections.Generic;
using SoliUndo.CardsStack;
using SoliUndo.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace SoliUndo
{
    public class SolitaireGameManager : MonoBehaviour
    {
        [Header("Game Configuration")]
        [SerializeField] private SoliUndoConfig config; 
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private List<CardStack> cardStacks;
    
        [Header("UI References")]
        [SerializeField] private Button undoButton;
        
        
        private List<Card> _allCards = new();
        private CardsInitializer  _cardsInitializer;
        private CardsStacksDistributor _cardsStacksDistributor;

        
        private void Start()
        {
            InitializeGame();
            SetupUI();
        }
        
        private void InitializeGame()
        {
            if (config == null || cardPrefab == null)
            {
                Debug.LogError("Missing required references in SolitaireGameManager");
                return;
            }

            _cardsInitializer = new CardsInitializer();
            _cardsStacksDistributor  = new CardsStacksDistributor();
            
            InitializeNewSetOfCards();
        }

        private void InitializeNewSetOfCards()
        { 
            _allCards.Clear();
            _allCards = _cardsInitializer.CreateCards(cardPrefab, config.CardsData);
            _cardsStacksDistributor.DealCards(_allCards, cardStacks);
        }
        
        private void SetupUI()
        {
            if (undoButton != null)
            {
                undoButton.onClick.AddListener(UndoLastMove);
            }
        }

        private void UndoLastMove()
        {

        }
        
        public void OnDestroy()
        {
            DestroyCards();
            
            if (undoButton != null)
            {
                undoButton.onClick.RemoveAllListeners();
            }
        }
        
        private void DestroyCards()
        {
            for (var i = _allCards.Count - 1; i >= 0; i--)
            {
                var card = _allCards[i];
                if (card != null)
                {
                    Destroy(card.gameObject);
                }
            }
            _allCards.Clear();
        }

    }
}