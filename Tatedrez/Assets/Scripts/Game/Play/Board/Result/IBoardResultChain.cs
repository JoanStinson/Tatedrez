namespace JGM.Game
{
    public interface IBoardResultChain
    {
        IBoardResultChain NextChain { get; }

        bool CheckResult(BoardModel boardModel);
    }
}
