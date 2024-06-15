namespace JGM.Game
{
    public class BoardController
    {
        private readonly BoardModel m_boardModel;
        private readonly BoardResultController m_boardResultController;

        public BoardController(BoardModel boardModel)
        {
            m_boardModel = boardModel;
            m_boardResultController = new BoardResultController();
        }

        public bool CellIsValidForPiece(CellView cell, PieceView piece)
        {
            return cell.IsEmpty;
        }

        public bool CheckTicTacToe()
        {
            return m_boardResultController.CheckTicTactoe(m_boardModel);
        }
    }
}
