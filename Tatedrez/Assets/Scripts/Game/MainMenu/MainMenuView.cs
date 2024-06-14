using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JGM.Game
{
    public class MainMenuView : ScreenView
    {
        [SerializeField] private Button m_playVersusPlayerButton;
        [SerializeField] private Button m_playVersusCpuButton;
        [SerializeField] private Button m_exitGameButton;
        [SerializeField] private Button m_changeLanguageButton;

        [Inject]
        private IAudioService m_audioService;
        private GameView m_gameView;
        private MainMenuController m_controller;

        public void Initialize(GameView gameView, MainMenuController controller)
        {
            m_gameView = gameView;
            m_controller = controller;

            m_playVersusPlayerButton.onClick.AddListener(OnClickPlayVersusPlayerButton);
            m_playVersusCpuButton.onClick.AddListener(OnClickPlayerVersusCpuButton);
            m_exitGameButton.onClick.AddListener(OnClickExitGameButton);
            m_changeLanguageButton.onClick.AddListener(OnClickChangeLanguageButton);
        }

        private void OnClickPlayVersusPlayerButton()
        {
            m_gameView.OnClickPlayVersusPlayerButton();
            m_audioService.Play(AudioFileNames.ButtonClickSfx);
        }

        private void OnClickPlayerVersusCpuButton()
        {
            m_gameView.OnClickPlayerVersusCpuButton();
            m_audioService.Play(AudioFileNames.ButtonClickSfx);
        }

        private void OnClickExitGameButton()
        {
            m_gameView.OnClickExitGameButton();
            m_audioService.Play(AudioFileNames.ButtonClickSfx);
        }

        private void OnClickChangeLanguageButton()
        {
            m_controller.ChangeLanguageToRandom();
            m_audioService.Play(AudioFileNames.ButtonClickSfx);
        }
    }
}
