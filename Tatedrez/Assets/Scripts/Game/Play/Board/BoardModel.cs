using UnityEngine;

namespace JGM.Game
{
    public class BoardModel
    {
        private readonly CellModel[,] m_grid;
        private readonly int m_rows;
        private readonly int m_columns;

        public BoardModel(int rows, int columns)
        {
            Debug.Assert(rows > 0 && columns > 0);
            m_rows = rows;
            m_columns = columns;
            m_grid = new CellModel[3, 3];
            ClearCells();
        }

        private void ClearCells()
        {
            for (int i = 0; i < m_rows; i++)
            {
                for (int j = 0; j < m_columns; j++)
                {
                    m_grid[i, j] = null;
                }
            }
        }
    }
}
