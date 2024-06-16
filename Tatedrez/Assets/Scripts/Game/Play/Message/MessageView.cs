using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace JGM.Game
{
    public class MessageView : MonoBehaviour
    {
        [SerializeField] private LocalizedText m_messageText;
        [SerializeField] private int m_animationPositionInY = 2000;
        [SerializeField] private float m_animationDuration = 1f;

        public async void ShowMessage(int playerId)
        {
            m_messageText.SetIntegerValue(playerId);
            gameObject.SetActive(true);
            var rectTransform = (RectTransform)gameObject.transform;
            rectTransform.DOAnchorPos(new Vector2(0, m_animationPositionInY), 0);
            rectTransform.DOAnchorPos(Vector2.zero, m_animationDuration);
            await Task.Delay(TimeSpan.FromSeconds(m_animationDuration));
        }

        public async void HideMessage(bool animate)
        {
            if (!animate)
            {
                gameObject.SetActive(false);
                return;
            }

            var rectTransform = (RectTransform)gameObject.transform;
            rectTransform.DOAnchorPos(new Vector2(0, m_animationPositionInY), m_animationDuration);
            await Task.Delay(TimeSpan.FromSeconds(m_animationDuration));
            gameObject.SetActive(false);
        }
    }
}
