using UnityEngine;
using UnityEngine.EventSystems;

namespace JGM.Game
{
    public class PieceController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private CellView[] m_boardCells;
        private RectTransform m_canvasRect;
        private RectTransform m_rectTransform;

        private Vector2 m_originalPosition;
        private Transform m_originalParent;
        private CellView m_lastHighlightedCell;

        public void Initialize(CellView[] boardCells, RectTransform canvasRect)
        {
            m_boardCells = boardCells;
            m_canvasRect = canvasRect;
            m_rectTransform = (RectTransform)transform;
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

        private void SetPiecePositionToMousePosition(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_canvasRect, eventData.position, eventData.pressEventCamera, out var localPoint);
            m_rectTransform.localPosition = localPoint;
        }

        private void HighlightSlotIfPositionValid(PointerEventData eventData)
        {
            m_lastHighlightedCell = null;

            foreach (CellView boardCell in m_boardCells)
            {
                var boardCellRect = (RectTransform)boardCell.transform;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(boardCellRect, eventData.position, eventData.pressEventCamera, out var slotLocalPoint);

                if (boardCellRect.rect.Contains(slotLocalPoint))
                {
                    boardCell.SetHighlightedColor();
                    m_lastHighlightedCell = boardCell;
                }
                else
                {
                    boardCell.SetDefaultColor();
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            foreach (var slot in m_boardCells)
            {
                slot.SetDefaultColor();
            }

            if (m_lastHighlightedCell != null)
            {
                m_rectTransform.SetParent(m_lastHighlightedCell.transform, false);
                m_rectTransform.localPosition = Vector3.zero;
            }
            else
            {
                ReturnToOriginalPosition();
            }

            m_lastHighlightedCell = null;
        }

        private void ReturnToOriginalPosition()
        {
            m_rectTransform.SetParent(m_originalParent, false);
            m_rectTransform.anchoredPosition = m_originalPosition;
        }
    }
}
