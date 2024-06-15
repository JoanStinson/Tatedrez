namespace JGM.Game
{
    public class PlayController
    {
        private readonly GameModel m_gameModel;
        private int m_playerTurn;

        public PlayController(GameModel gameModel)
        {
            m_gameModel = gameModel;
        }

        public int StartNewGame()
        {
            m_playerTurn = 0;
            return m_playerTurn;
        }

        public int ChangePlayerTurn()
        {
            m_playerTurn ^= 1;
            return m_playerTurn;
        }

        public int GetPlayerTurn()
        {
            return m_playerTurn;
        }
    }
}
