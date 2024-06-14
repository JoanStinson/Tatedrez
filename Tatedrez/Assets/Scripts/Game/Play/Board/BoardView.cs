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

            for (int i = 0; i < m_cells.Length; i++)
            {
                if (i % 2 == 0)
                {
                    var lightBrownCell = new CellModel(null, m_cellDarkBrownColor, m_cellHighlightedColor);
                    m_cells[i].Initialize(lightBrownCell);
                }
                else
                {
                    var darkBrownCell = new CellModel(null, m_cellLightBrownColor, m_cellHighlightedColor);
                    m_cells[i].Initialize(darkBrownCell);
                }
            }
        }
    }
}
