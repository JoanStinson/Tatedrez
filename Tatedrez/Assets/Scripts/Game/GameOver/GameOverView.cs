using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class GameOverView : ScreenView
    {
        [SerializeField] private LocalizedText m_playerWinsText;
        [SerializeField] private Button m_playAgainButton;
        [SerializeField] private Button m_mainMenuButton;
        [SerializeField] private Button m_exitGameButton;

        private GameView m_gameView;

        public override void Initialize(GameView gameView)
        {
            m_gameView = gameView;
            m_playAgainButton.onClick.AddListener(OnClickPlayAgainButton);
            m_mainMenuButton.onClick.AddListener(OnClickMainMenuButton);
            m_exitGameButton.onClick.AddListener(OnClickExitGameButton);
        }

        private void OnClickPlayAgainButton()
        {
            m_gameView.OnClickPlayAgainButton();
        }

        private void OnClickMainMenuButton()
        {
            m_gameView.OnClickMainMenuButton();
        }

        private void OnClickExitGameButton()
        {
            m_gameView.OnClickExitGameButton();
        }

        public override void Show()
        {
            base.Show();
            m_playerWinsText.SetIntegerValue(m_gameView.Model.LastPlayerWinId + 1);
        }
    }
}