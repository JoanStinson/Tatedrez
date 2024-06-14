using UnityEngine;

namespace JGM.Game
{
    public class Tatedrez : MonoBehaviour
    {
        [SerializeField]
        private GameView m_gameView;

        private void Start()
        {
            Run();
        }

        private void Run()
        {
            m_gameView.Initialize(new GameController());
        }
    }
}