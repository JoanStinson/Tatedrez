using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class PieceView : MonoBehaviour
    {
        public PieceModel Model { get; private set; }

        [SerializeField]
        private Image m_image;

        private PieceController m_controller;

        public void Initialize(PieceModel pieceModel, CellView[] boardCells, RectTransform canvasRect)
        {
            Model = pieceModel;
            m_image.sprite = pieceModel.Sprite;
            m_controller = gameObject.AddComponent<PieceController>();
            m_controller.Initialize(boardCells, canvasRect);
        }

        public void SetEnabledColorAlpha()
        {
            m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, Model.EnabledColorAlpha);
        }

        public void SetDisabledColorAlpha()
        {
            m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, Model.DisabledColorAlpha);
        }
    }
}
