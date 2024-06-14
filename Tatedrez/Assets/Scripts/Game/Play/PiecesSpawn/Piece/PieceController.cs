using UnityEngine;
using UnityEngine.EventSystems;

namespace JGM.Game
{
    public class PieceController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform m_rectTransform;
        private RectTransform m_canvasRect;
        private CellView[] m_boardCells;

        private Vector2 m_originalPosition;
        private Transform m_originalParent;
        private CellView m_lastHighlightedSlot;

        public void Initialize(CellView[] boardCells, RectTransform canvasRect)
        {
            m_rectTransform = (RectTransform)transform;
            m_canvasRect = canvasRect;
            m_boardCells = boardCells;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_originalPosition = m_rectTransform.anchoredPosition;
            m_originalParent = m_rectTransform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            m_rectTransform.SetParent(m_rectTransform.root, true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            SetPiecePositionToMousePosition(eventData);
            HighlightSlotIfPositionValid(eventData);
        }

        private void HighlightSlotIfPositionValid(PointerEventData eventData)
        {
            m_lastHighlightedSlot = null;

            foreach (CellView slotView in m_boardCells)
            {
                var slotRect = (RectTransform)slotView.transform;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(slotRect, eventData.position, eventData.pressEventCamera, out var slotLocalPoint);

                if (slotRect.rect.Contains(slotLocalPoint))
                {
                    slotView.SetHighlightedColor();
                    m_lastHighlightedSlot = slotView;
                }
                else
                {
                    slotView.SetDefaultColor();
                }
            }
        }

        private void SetPiecePositionToMousePosition(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_canvasRect, eventData.position, eventData.pressEventCamera, out var localPoint);
            m_rectTransform.localPosition = localPoint;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            foreach (var slot in m_boardCells)
            {
                slot.SetDefaultColor();
            }

            if (m_lastHighlightedSlot != null)
            {
                m_rectTransform.SetParent(m_lastHighlightedSlot.transform, false);
                m_rectTransform.localPosition = Vector3.zero;
            }
            else
            {
                ReturnToOriginalPosition();
            }

            m_lastHighlightedSlot = null;
        }

        private void ReturnToOriginalPosition()
        {
            m_rectTransform.SetParent(m_originalParent, false);
            m_rectTransform.anchoredPosition = m_originalPosition;
        }
    }
}
