using System.Collections.Generic;

namespace JGM.Game
{
    public class BoardMovementController
    {
        private readonly Dictionary<GameSettings.PieceType, IMovementValidator> m_movementValidators;

        public BoardMovementController()
        {
            m_movementValidators = new Dictionary<GameSettings.PieceType, IMovementValidator>()
            {
                { GameSettings.PieceType.Knight, new KnightMovementValidator() },
                { GameSettings.PieceType.Rook,   new RookMovementValidator() },
                { GameSettings.PieceType.Bishop, new BishopMovementValidator() }
            };
        }

        public bool CellIsValidForPiece(CellView cell, PieceView piece, BoardModel boardModel)
        {
            bool allPiecesAreNotPlaced = boardModel.GetPiecesOnBoard() < boardModel.Rows + boardModel.Columns;
            if (allPiecesAreNotPlaced)
            {
                return cell.IsEmpty;
            }

            return m_movementValidators[piece.Model.PieceType].CellIsValidForPiece(cell, piece);
        }
    }
}
