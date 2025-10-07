using System;
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
        [Header("Detection Settings")]
        [SerializeField] private LayerMask cardStackLayerMask = -1;
        
        
        private CardData _cardData;
        private CardStack _currentStack;
        private Sprite _cardBackArt;
        private bool _isFaceUp;
        
        private bool _isDragging;
        private Vector3 _originalPosition;
        private Vector3 _offset;
        
        public Vector3 OriginalPosition => _originalPosition;
        public CardData CardData => _cardData;
        public Action<Card, CardStack, CardStack> OnMoveCard;
        

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
        
        #region Drag Implementation
    
        private void OnMouseDown()
        {
            if (!_isFaceUp || cardCollider == null) return;
        
            _isDragging = true;
            _originalPosition = transform.position;
        
            var mouseWorldPos = GetMousePosition(0.01f);
            _offset = transform.position - mouseWorldPos;
            cardCollider.enabled = false;
        }
    
        private void OnMouseUp()
        {
            if (!_isDragging) return;
        
            _isDragging = false;
            if (cardCollider != null)
            {
                cardCollider.enabled = true;
            }
        
            var targetStack = FindTargetStack();
            if (CanPlaceCardInStack(targetStack))
            {
                OnMoveCard?.Invoke(this, _currentStack, targetStack);
            }
            else
            {
                transform.position = _originalPosition;
            }
        }

        private bool CanPlaceCardInStack(CardStack targetStack)
        {
            return targetStack != null && targetStack != _currentStack && targetStack.CanPlaceCard(this);
        }
    
        private void Update()
        {
            if (_isDragging)
            {
                var mouseWorldPos = GetMousePosition();
                transform.position = mouseWorldPos + _offset;
            }
        }

        private Vector3 GetMousePosition(float zOffset = 0)
        {
            var mouseWorldPos = MouseWorldPos;
            mouseWorldPos.z = transform.position.z + zOffset;
            return mouseWorldPos;
        }
    
        private CardStack FindTargetStack()
        {
            var mouseWorldPos = MouseWorldPos;
            mouseWorldPos.z = 0;
    
            var hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, cardStackLayerMask);
    
            if (hit.collider != null)
            {
                var stack = hit.collider.GetComponent<CardStack>();
                if (stack != null) return stack;
            }
    
            return null;
        }
        
        private Vector3 MouseWorldPos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    
        #endregion
    }
}
