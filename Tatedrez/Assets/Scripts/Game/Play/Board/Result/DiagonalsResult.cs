namespace JGM.Game
{
    public class DiagonalsResult : IBoardResultChain
    {
        public IBoardResultChain NextChain { get; set; }

        public bool CheckResult(BoardModel boardModel)
        {
            return false;
        }
    }
}
