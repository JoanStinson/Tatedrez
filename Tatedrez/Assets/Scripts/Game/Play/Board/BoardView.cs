using UnityEngine;

namespace JGM.Game
{
    public class BoardView : MonoBehaviour
    {
        public CellView[] Cells => m_cells;

        [SerializeField] private CellView[] m_cells;
        [SerializeField] private Color m_cellLightBrownColor;
        [SerializeField] private Color m_cellDarkBrownColor;
        [SerializeField] private Color m_cellHighlightedColor;

        private BoardModel m_boardModel;

        public void Initialize(BoardModel boardModel)
        {
            m_boardModel = boardModel;

            int currentRow = 0;
            int currentColumn = -1;
            for (int i = 0; i < m_cells.Length; i++)
            {
                var boardCellColor = (i % 2 == 0) ? m_cellDarkBrownColor : m_cellLightBrownColor;
                var boardCell = new CellModel(null, boardCellColor, m_cellHighlightedColor);
                m_cells[i].Initialize(boardCell);

                if (++currentColumn > 2)
                {
                    currentColumn = 0;
                    currentRow++;
                }

                m_boardModel.SetCell(currentRow, currentColumn, boardCell);
            }
        }
    }
}
