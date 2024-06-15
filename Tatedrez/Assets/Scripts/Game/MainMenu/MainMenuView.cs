using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class MainMenuView : ScreenView
    {
        [SerializeField] private Button m_playVersusPlayerButton;
        [SerializeField] private Button m_playVersusCpuButton;
        [SerializeField] private Button m_exitGameButton;
        [SerializeField] private Button m_changeLanguageButton;

        private GameView m_gameView;

        public override void Initialize(GameView gameView)
        {
            m_gameView = gameView;
            m_playVersusPlayerButton.onClick.AddListener(OnClickPlayVersusPlayerButton);
            m_playVersusCpuButton.onClick.AddListener(OnClickPlayVersusCpuButton);
            m_exitGameButton.onClick.AddListener(OnClickExitGameButton);
            m_changeLanguageButton.onClick.AddListener(OnClickChangeLanguageButton);
        }

        private void OnClickPlayVersusPlayerButton()
        {
            m_gameView.OnClickPlayVersusPlayerButton();
        }

        private void OnClickPlayVersusCpuButton()
        {
            m_gameView.OnClickPlayVersusCpuButton();
        }

        private void OnClickExitGameButton()
        {
            m_gameView.OnClickExitGameButton();
        }

        private void OnClickChangeLanguageButton()
        {
            m_gameView.OnClickChangeLanguageButton();
        }
    }
}
