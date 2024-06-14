using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class CellView : MonoBehaviour
    {
        [SerializeField]
        private Image m_image;

        private CellModel m_cellModel;

        public void Initialize(CellModel cellModel)
        {
            m_cellModel = cellModel;
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
