using UnityEngine;

namespace JGM.Game
{
    public class MessageView : MonoBehaviour
    {
        [SerializeField]
        private LocalizedText m_messageText;

        public void ShowMessage(int playerId)
        {
            m_messageText.SetIntegerValue(playerId);
            gameObject.SetActive(true);
        }

        public void HideMessage()
        {
            gameObject.SetActive(false);
        }
    }
}
