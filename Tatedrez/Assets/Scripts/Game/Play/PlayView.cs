using UnityEngine;

namespace JGM.Game
{
    public class PlayView : ScreenView
    {
        [SerializeField] private LocalizedText m_playerTurnText;
        [SerializeField] private BoardView m_boardView;
        [SerializeField] private PiecesSpawnView m_leftPiecesSpawnView;
        [SerializeField] private PiecesSpawnView m_rightPiecesSpawnView;
        
        private GameView m_gameView;
        private PlayController m_controller;

        public void Initialize(GameView gameView, GameModel gameModel, PlayController controller)
        {
            m_gameView = gameView;
            m_controller = controller;

            m_boardView.Initialize(new BoardModel(3, 3));
            var canvasRect = (RectTransform)m_gameView.Canvas.transform;
            m_leftPiecesSpawnView.Initialize(gameModel.Player1PieceConfigs, m_boardView.Cells, canvasRect);
            m_rightPiecesSpawnView.Initialize(gameModel.Player2PieceConfigs, m_boardView.Cells, canvasRect);
        }
    }
}
