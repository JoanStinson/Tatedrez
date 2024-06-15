using UnityEngine;
using UnityEngine.EventSystems;

namespace JGM.Game
{
    public class BoardController
    {
        private readonly BoardModel m_boardModel;

        public BoardController(BoardModel boardModel)
        {
            m_boardModel = boardModel;
        }

        public bool CellIsValidForPiece(CellView cell, PieceView pieceView)
        {
            return cell.IsEmpty;
        }

        public bool CheckTicTacToe()
        {
            return false;
        }
    }
}
