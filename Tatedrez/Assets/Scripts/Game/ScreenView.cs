using UnityEngine;

namespace JGM.Game
{
    public abstract class ScreenView : MonoBehaviour
    {
        public abstract void Initialize(GameView gameView);

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
