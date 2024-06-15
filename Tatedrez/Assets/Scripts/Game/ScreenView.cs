using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace JGM.Game
{
    public abstract class ScreenView : MonoBehaviour
    {
        [SerializeField]
        protected CanvasGroup m_canvasGroup;

        private const int m_showPosition = -2000;
        private const int m_hidePosition = 2000;
        private const float m_showDuration = 1f;
        private const float m_hideDuration = 1f;

        public abstract void Initialize(GameView gameView);

        public virtual async void Show()
        {
            m_canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(true);
            var rectTransform = (RectTransform)gameObject.transform;
            rectTransform.DOAnchorPos(new Vector2(m_showPosition, 0), 0);
            rectTransform.DOAnchorPos(Vector2.zero, m_showDuration);
            await Task.Delay(TimeSpan.FromSeconds(m_showDuration));
            m_canvasGroup.blocksRaycasts = true;
        }

        public virtual async void Hide()
        {
            m_canvasGroup.blocksRaycasts = false;
            var rectTransform = (RectTransform)gameObject.transform;
            rectTransform.DOAnchorPos(new Vector2(m_hidePosition, 0), m_hideDuration);
            await Task.Delay(TimeSpan.FromSeconds(m_hideDuration));
            gameObject.SetActive(false);
        }
    }
}
