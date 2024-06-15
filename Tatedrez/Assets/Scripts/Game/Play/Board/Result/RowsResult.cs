namespace JGM.Game
{
    public class RowsResult : IBoardResultChain
    {
        public IBoardResultChain NextChain { get; set; }

        public bool CheckResult(BoardModel boardModel)
        {
            return NextChain.CheckResult(boardModel);
        }
    }
}
