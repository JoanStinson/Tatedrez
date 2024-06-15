using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class CellView : MonoBehaviour
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public bool IsEmpty => m_cellModel.PieceModel == null;
        public PieceModel PieceModel => m_cellModel.PieceModel;

        [SerializeField]
        private Image m_image;

        private CellModel m_cellModel;

        public void Initialize(CellModel cellModel, int row, int column)
        {
            m_cellModel = cellModel;
            Row = row;
            Column = column;
            SetDefaultColor();
        }

        public void SetDefaultColor()
        {
            m_image.color = m_cellModel.DefaultColor;
        }

        public void SetHighlightedColor()
        {
            m_image.color = m_cellModel.HighlightedColor;
        }
    }
}
