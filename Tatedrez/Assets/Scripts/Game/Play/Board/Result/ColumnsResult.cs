namespace JGM.Game
{
    public class ColumnsResult : IBoardResultChain
    {
        public IBoardResultChain NextChain { get; set; }

        public bool CheckResult(BoardModel boardModel)
        {
            return NextChain.CheckResult(boardModel);
        }
    }
}
