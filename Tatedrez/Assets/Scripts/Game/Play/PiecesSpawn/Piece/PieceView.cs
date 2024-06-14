using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JGM.Game
{
    public class PieceView : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public PieceModel Model { get; private set; }

        [SerializeField]
        private Image m_image;

        private PieceController m_pieceController;

        public void Initialize(PieceModel pieceModel, BoardView boardView, RectTransform canvasRect)
        {
            Model = pieceModel;
            m_image.sprite = pieceModel.Sprite;
            m_pieceController = new PieceController(pieceModel, canvasRect, boardView, (RectTransform)transform);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_pieceController.OnPointerDown(eventData);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            m_pieceController.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            m_pieceController.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            m_pieceController.OnEndDrag(eventData);
        }

        public void EnableInteraction()
        {
            m_image.raycastTarget = true;
            m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, Model.EnabledColorAlpha);
        }

        public void DisableInteraction()
        {
            m_image.raycastTarget = false;
            m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, Model.DisabledColorAlpha);
        }
    }
}
