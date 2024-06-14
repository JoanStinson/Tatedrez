using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class SlotView : MonoBehaviour
    {
        [SerializeField] private Image m_image;
        [SerializeField] private Color m_defaultColor;
        [SerializeField] private Color m_highlightedColor;

        public void SetDefaultColor()
        {
            m_image.color = m_defaultColor;
        }

        public void SetHighlightedColor()
        {
            m_image.color= m_highlightedColor;
        }
    }
}
