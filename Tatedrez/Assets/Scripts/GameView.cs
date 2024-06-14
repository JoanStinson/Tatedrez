using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private MainMenuView m_mainMenuView;
        //[SerializeField] private PlayView m_playView;
        [SerializeField] private GameOverView m_gameOverView;

        [Inject] private ILocalizationService m_localizationService;
        [Inject] private GameSettings m_gameSettings;

        private GameController m_gameController;
        //private GameModel m_gameModel;

        public void Initialize(GameController gameController)
        {
            m_gameController = gameController;

            m_mainMenuView.Initialize(this, new MainMenuController(m_localizationService));
            m_mainMenuView.Show();

            //m_gameModel = m_gameController.GetGameModel(m_gameSettings);
            //m_playView.Initialize(this, m_gameModel);
            //m_playView.Hide();

            m_gameOverView.Initialize(this, null);
            m_gameOverView.Hide();
        }

        public void OnClickPlayVersusPlayerButton()
        {
            m_mainMenuView.Hide();
            //pasarle por aqui el userinput o botinput
            //m_playView.Show();
        }

        public void OnClickPlayerVersusCpuButton()
        {
            m_mainMenuView.Hide();
            //pasarle por aqui el userinput o botinput
            //m_playView.Show();
        }

        public void OnClickExitGameButton()
        {
            m_gameController.ExitGame();
        }

        public void OnClickPlayAgainButton()
        {
            m_gameOverView.Hide();
            //pasarle por aqui el userinput o botinput
            //m_playView.Show();
        }

        public void OnClickMainMenuButton()
        {
            m_gameOverView.Hide();
            m_mainMenuView.Show();
        }
    }
}