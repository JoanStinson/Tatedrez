using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class PieceView : MonoBehaviour
    {
        public string Id { get; private set; }

        [SerializeField] private Image m_image;

        private PieceController m_controller;

        public void Initialize(PieceModel pieceModel, RectTransform canvasRect, SlotView[] slotViews)
        {
            Id = pieceModel.Id;
            m_image.sprite = pieceModel.Sprite;
            m_controller = gameObject.AddComponent<PieceController>();
            m_controller.Initialize(canvasRect, slotViews);
        }
    }
}
