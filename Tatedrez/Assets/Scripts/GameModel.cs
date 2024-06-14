using static JGM.Game.GameSettings;

namespace JGM.Game
{
    public class GameModel
    {
        public PieceConfig[] Player1PieceConfigs { get; private set; }
        public PieceConfig[] Player2PieceConfigs { get; private set; }

        public GameModel(GameSettings gameSettings)
        {
            Player1PieceConfigs = gameSettings.Player1PieceConfigs;
            Player2PieceConfigs = gameSettings.Player2PieceConfigs;
        }
    }
}