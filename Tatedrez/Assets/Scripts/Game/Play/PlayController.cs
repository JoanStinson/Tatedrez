using UnityEngine;

namespace JGM.Game
{
    public class PlayController
    {
        private int m_playerTurn;

        public void GenerateFirstTurnRandomly()
        {
            m_playerTurn = Random.Range(0, 2);
        }

        public void ChangePlayerTurn()
        {
            m_playerTurn ^= 1;
        }

        public int GetPlayerTurn()
        {
            return m_playerTurn;
        }

        public int GetNonPlayerTurn()
        {
            return m_playerTurn ^ 1;
        }
    }
}
