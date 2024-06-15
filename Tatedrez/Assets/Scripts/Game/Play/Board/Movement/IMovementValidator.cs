namespace JGM.Game
{
    public interface IMovementValidator
    {
        bool CellIsValidForPiece(CellView cell, PieceView piece);
    }
}
