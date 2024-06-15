using UnityEngine;
using static JGM.Game.GameSettings;

namespace JGM.Game
{
    public class GameModel
    {
        public int BoardRows { get; private set; }
        public int BoardColumns { get; private set; }
        public Color BoardCellDarkBrownColor { get; private set; }
        public Color BoardCellLightBrownColor { get; private set; }
        public Color BoardCellHighlightedColor { get; private set; }
        public float PieceEnabledColorAlpha { get; private set; }
        public float PieceDisabledColorAlpha { get; private set; }
        public int LastPlayerWinId { get; set; }

        private readonly PieceConfig[] m_player1PieceConfigs;
        private readonly PieceConfig[] m_player2PieceConfigs;

        public GameModel(GameSettings gameSettings)
        {
            m_player1PieceConfigs = gameSettings.Player1PieceConfigs;
            m_player2PieceConfigs = gameSettings.Player2PieceConfigs;
            BoardRows = gameSettings.BoardRows;
            BoardColumns = gameSettings.BoardColumns;
            BoardCellDarkBrownColor = gameSettings.BoardCellDarkBrownColor;
            BoardCellLightBrownColor = gameSettings.BoardCellLightBrownColor;
            BoardCellHighlightedColor = gameSettings.BoardCellHighlightedColor;
            PieceEnabledColorAlpha = gameSettings.PieceEnabledColorAlpha;
            PieceDisabledColorAlpha = gameSettings.PieceDisabledColorAlpha;
        }

        public PieceConfig[] GetPieceConfigs(int index)
        {
            if (index == 0)
            {
                return m_player1PieceConfigs;
            }

            return m_player2PieceConfigs;
        }
    }
}