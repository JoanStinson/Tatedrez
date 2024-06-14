using UnityEngine;
using UnityEngine.EventSystems;

namespace JGM.Game
{
    public class BoardController
    {
        private readonly BoardModel m_boardModel;
        private readonly CellView[] m_cells;

        public BoardController(BoardModel boardModel, CellView[] cells)
        {
            m_boardModel = boardModel;
            m_cells = cells;
        }

        public void HighlightValidCellForPiece(PieceModel pieceModel, PointerEventData eventData)
        {
            foreach (var cell in m_cells)
            {
                var cellRect = (RectTransform)cell.transform;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(cellRect, eventData.position, eventData.pressEventCamera, out var cellLocalPoint);

                if (CellIsValidForPiece(cell, cellRect, cellLocalPoint))
                {
                    cell.SetHighlightedColor();
                }
                else
                {
                    cell.SetDefaultColor();
                }
            }
        }

        private bool CellIsValidForPiece(CellView boardCell, RectTransform boardCellRect, Vector2 cellLocalPoint)
        {
            return boardCellRect.rect.Contains(cellLocalPoint) /*&& boardCell.Model.IsEmpty()*/;
        }

        public bool PieceCanBePutInCell(PieceModel pieceModel, PointerEventData eventData, ref CellView validCell)
        {
            foreach (var cell in m_cells)
            {
                var cellRect = (RectTransform)cell.transform;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(cellRect, eventData.position, eventData.pressEventCamera, out var cellLocalPoint);

                if (CellIsValidForPiece(cell, cellRect, cellLocalPoint))
                {
                    validCell = cell;
                }

                cell.SetDefaultColor();
            }

            return validCell != null;
        }
    }
}
