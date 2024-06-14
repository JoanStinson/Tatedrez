namespace JGM.Game
{
    public class GameController
    {
        private GameModel m_gameModel;

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public GameModel GetGameModel(GameSettings gameSettings)
        {
            m_gameModel ??= new GameModel(gameSettings);
            return m_gameModel;
        }
    }
}