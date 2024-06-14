using UnityEngine;
using UnityEngine.EventSystems;

namespace JGM.Game
{
    public class PieceController
    {
        private readonly PieceModel m_pieceModel;
        private readonly RectTransform m_canvasRect;
        private readonly BoardView m_boardView;
        private readonly RectTransform m_rectTransform;

        private Vector2 m_startingPosition;
        private Transform m_startingParent;

        public PieceController(PieceModel pieceModel, RectTransform canvasRect, BoardView boardView, RectTransform rectTransform)
        {
            m_pieceModel = pieceModel;
            m_canvasRect = canvasRect;
            m_boardView = boardView;
            m_rectTransform = rectTransform;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_startingPosition = m_rectTransform.anchoredPosition;
            m_startingParent = m_rectTransform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            m_rectTransform.SetParent(m_rectTransform.root, true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            SetPiecePositionToMousePosition(eventData);
            m_boardView.HighlightValidCellForPiece(m_pieceModel, eventData);
        }

        private void SetPiecePositionToMousePosition(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_canvasRect, eventData.position, eventData.pressEventCamera, out var localPoint);
            m_rectTransform.localPosition = localPoint;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (m_boardView.PieceCanBePutInCell(m_pieceModel, eventData, out var validCell))
            {
                PutPieceInCell(validCell);
            }
            else
            {
                ReturnPieceToStartingPosition();
            }
        }

        private void PutPieceInCell(CellView cellView)
        {
            m_rectTransform.SetParent(cellView.transform, false);
            m_rectTransform.localPosition = Vector3.zero;
        }

        private void ReturnPieceToStartingPosition()
        {
            m_rectTransform.SetParent(m_startingParent, false);
            m_rectTransform.anchoredPosition = m_startingPosition;
        }
    }
}
