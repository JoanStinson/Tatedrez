using UnityEngine;

namespace JGM.Game
{
    public class PlayController
    {
        private int m_playerTurn;

        public int MakeStartingTurnRandom()
        {
            m_playerTurn = Random.Range(0, 2);
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
