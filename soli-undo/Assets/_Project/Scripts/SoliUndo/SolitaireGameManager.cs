using System.Collections.Generic;
using SoliUndo.CardsStack;
using SoliUndo.MoveCommand;
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
        private UndoManager _undoManager;


        private void Awake()
        {
            if (config == null || cardPrefab == null)
            {
                Debug.LogError("Missing required references in SolitaireGameManager");
                return;
            }

            _cardsInitializer = new CardsInitializer();
            _cardsStacksDistributor = new CardsStacksDistributor();
            _undoManager = new UndoManager(config.MaxUndoSteps);
        }

        private void Start()
        {
            InitializeNewSetOfCards();
            SetupUI();
        }

        private void InitializeNewSetOfCards()
        {
            DestroyCards();
            
            _allCards = _cardsInitializer.CreateCards(cardPrefab, config.CardsData);
            _cardsStacksDistributor.DealCards(_allCards, cardStacks);

            foreach (var card in _allCards)
            {
                card.OnMoveCard += MoveCard;
            }
        }
        
        private void SetupUI()
        {
            if (undoButton != null)
            {
                undoButton.onClick.AddListener(UndoLastMove);
            }
        }
        
        private void MoveCard(Card card, CardStack fromStack, CardStack toStack)
        {
            if (card == null || fromStack == null || toStack == null)
            {
                Debug.LogError("Invalid move parameters");
                return;
            }
            
            if (!toStack.CanPlaceCard(card))
            {
                Debug.Log("Invalid move: card cannot be placed on target stack");
                return;
            }
            
            _undoManager.ExecuteCommand( new MoveCardCommand(card, fromStack, toStack));
        }

        private void UndoLastMove()
        {
            _undoManager.UndoLastCommand();
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
                    card.OnMoveCard -= MoveCard;
                    Destroy(card.gameObject);
                }
            }
            _allCards.Clear();
        }

    }
}