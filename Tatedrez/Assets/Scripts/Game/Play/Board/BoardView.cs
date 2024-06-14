using UnityEngine;
using UnityEngine.EventSystems;

namespace JGM.Game
{
    public class BoardView : MonoBehaviour
    {
        public CellView[] Cells => m_cells;

        [SerializeField]
        private CellView[] m_cells;

        private BoardModel m_boardModel;
        private BoardController m_boardController;

        public void Initialize(GameModel gameModel, BoardModel boardModel)
        {
            m_boardModel = boardModel;
            InitializeBoardCells(gameModel);
            m_boardController = new BoardController(boardModel, m_cells);
        }

        private void InitializeBoardCells(GameModel gameModel)
        {
            int currentRow = 0;
            int currentColumn = -1;

            for (int i = 0; i < m_cells.Length; i++)
            {
                var boardCellColor = (i % 2 == 0) ? gameModel.BoardCellDarkBrownColor : gameModel.BoardCellLightBrownColor;
                var boardCell = new CellModel(null, boardCellColor, gameModel.BoardCellHighlightedColor);
                m_cells[i].Initialize(boardCell);

                if (++currentColumn > 2)
                {
                    currentColumn = 0;
                    currentRow++;
                }

                m_boardModel.SetCell(currentRow, currentColumn, boardCell);
            }
        }

        public void HighlightValidCellForPiece(PieceModel pieceModel, PointerEventData eventData)
        {
            m_boardController.HighlightValidCellForPiece(pieceModel, eventData);
        }

        public bool PieceCanBePutInCell(PieceModel pieceModel, PointerEventData eventData, out CellView validCell)
        {
            validCell = null;
            return m_boardController.PieceCanBePutInCell(pieceModel, eventData, ref validCell);
        }
    }
}
