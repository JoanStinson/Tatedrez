using UnityEngine;
using UnityEngine.EventSystems;

namespace JGM.Game
{
    public class PieceController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private const int m_snappingDistance = 50;

        private RectTransform m_rectTransform;
        private RectTransform m_canvasRect;
        private SlotView[] m_slotViews;

        private Vector2 m_originalPosition;
        private Transform m_originalParent;

        public void Initialize(RectTransform canvasRect, SlotView[] slotViews)
        {
            m_rectTransform = (RectTransform)transform;
            m_canvasRect = canvasRect;
            m_slotViews = slotViews;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_originalPosition = m_rectTransform.anchoredPosition;
            m_originalParent = m_rectTransform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            m_rectTransform.SetParent(m_rectTransform.root, true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_canvasRect, eventData.position, eventData.pressEventCamera, out var localPoint);
            m_rectTransform.localPosition = localPoint;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ReturnToOriginalPosition();
        }

        private void ReturnToOriginalPosition()
        {
            m_rectTransform.SetParent(m_originalParent, false);
            m_rectTransform.anchoredPosition = m_originalPosition;
        }
    }
}
