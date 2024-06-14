using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JGM.Game
{
    public class GameOverView : ScreenView
    {
        [SerializeField] private Button m_playAgainButton;
        [SerializeField] private Button m_mainMenuButton;
        [SerializeField] private Button m_exitGameButton;

        [Inject]
        private IAudioService m_audioService;
        private GameView m_gameView;
        private GameModel m_gameModel;

        public void Initialize(GameView gameView, GameModel gameModel)
        {
            m_gameView = gameView;
            m_gameModel = gameModel;

            m_playAgainButton.onClick.AddListener(OnClickPlayAgainButton);
            m_mainMenuButton.onClick.AddListener(OnClickMainMenuButton);
            m_exitGameButton.onClick.AddListener(OnClickExitGameButton);
        }

        private void OnClickPlayAgainButton()
        {
            m_gameView.OnClickPlayAgainButton();
            m_audioService.Play(AudioFileNames.ButtonClickSfx);
        }

        private void OnClickMainMenuButton()
        {
            m_gameView.OnClickMainMenuButton();
            m_audioService.Play(AudioFileNames.ButtonClickSfx);
        }

        private void OnClickExitGameButton()
        {
            m_gameView.OnClickExitGameButton();
            m_audioService.Play(AudioFileNames.ButtonClickSfx);
        }
    }
}