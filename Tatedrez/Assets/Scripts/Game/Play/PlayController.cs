using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGM.Game
{
    public class PlayController
    {
        private GameModel m_gameModel;
        private int m_playerTurn;

        public PlayController(GameModel gameModel)
        {
            m_gameModel = gameModel;
        }

        public int StartNewGame()
        {
            return m_playerTurn;
            // turn
            // disable pieces from non-playerturn
            // ui text saying who's turn it is
        }
    }
}
