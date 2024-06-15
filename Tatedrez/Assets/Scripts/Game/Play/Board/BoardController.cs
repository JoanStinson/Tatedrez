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

        public void HighlightCellIfValidForPiece(PieceView pieceView, PointerEventData eventData)
        {
            foreach (var cell in m_cells)
            {
                var cellTransform = (RectTransform)cell.transform;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(cellTransform, eventData.position, eventData.pressEventCamera, out var cellLocalPoint);

                if (CellIsValidForPiece(cell, cellTransform, cellLocalPoint))
                {
                    cell.SetHighlightedColor();
                }
                else
                {
                    cell.SetDefaultColor();
                }
            }
        }

        private bool CellIsValidForPiece(CellView cell, RectTransform cellTransform, Vector2 cellLocalPoint)
        {
            return cellTransform.rect.Contains(cellLocalPoint) && cell.IsEmpty;
        }

        public bool PlacePieceOnBoard(PieceView pieceView, PointerEventData eventData, out CellView validCell)
        {
            validCell = null;

            foreach (var cell in m_cells)
            {
                var cellTransform = (RectTransform)cell.transform;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(cellTransform, eventData.position, eventData.pressEventCamera, out var cellLocalPoint);

                if (CellIsValidForPiece(cell, cellTransform, cellLocalPoint))
                {
                    pieceView.CellView?.RemovePiece();
                    validCell = cell;
                    validCell.SetPiece(pieceView);
                }

                cell.SetDefaultColor();
            }

            return validCell != null;
        }

        public bool CheckTicTacToe()
        {
            return false;
        }
    }
}
