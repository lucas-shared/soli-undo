using SoliUndo.CardsStack;
using SoliUndo.ScriptableObjects;
using UnityEngine;

namespace SoliUndo
{
    public class Card : MonoBehaviour
    {
        [Header("Card Components")]
        [SerializeField] private SpriteRenderer cardRenderer;
        [SerializeField] private SpriteRenderer backRenderer;
        [SerializeField] private Collider2D cardCollider;
        
        private CardData _cardData;
        private CardStack _currentStack;
        private Sprite _cardBackArt;
        private bool _isFaceUp;
        
        public void Initialize(CardData data, Sprite cardBackArt)
        {
            _cardData = data;
            _cardBackArt = cardBackArt;
            SetUpVisuals();
            UpdateVisuals();
        }
        
        public void SetCurrentStack(CardStack stack)
        {
            _currentStack = stack;
        }
        
        public void SetFaceUp(bool faceUp)
        {
            _isFaceUp = faceUp;
            UpdateVisuals();
        }

        private void SetUpVisuals()
        {
            if (_cardData == null || _cardBackArt == null) return;
            cardRenderer.sprite =  _cardData.Art;
            backRenderer.sprite = _cardBackArt;
        }

        private void UpdateVisuals()
        {
            cardRenderer.gameObject.SetActive(_isFaceUp);
            backRenderer.gameObject.SetActive(!_isFaceUp);
        }
    }
}
