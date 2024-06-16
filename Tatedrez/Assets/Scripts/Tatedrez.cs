using UnityEngine;

namespace JGM.Game
{
    public class Tatedrez : MonoBehaviour
    {
        [SerializeField]
        private GameView m_gameView;

        private void Start()
        {
            Application.targetFrameRate = 60;
            Run();
        }

        private void Run()
        {
            m_gameView.Initialize();
        }
    }
}