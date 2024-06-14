using UnityEngine;

namespace JGM.Game
{
    public class PlayView : ScreenView
    {
        [SerializeField] private LocalizedText m_playerTurnText;
        [SerializeField] private Transform m_player1PiecesParent;
        [SerializeField] private Transform m_player2PiecesParent;
        [SerializeField] private PieceView m_pieceViewPrefab;

        private GameView m_gameView;
        private GameModel m_gameModel;
        private PlayController m_controller;

        public void Initialize(GameView gameView, GameModel gameModel, PlayController controller)
        {
            m_gameView = gameView;
            m_gameModel = gameModel;
            m_controller = controller;

            foreach (var pieceConfig in m_gameModel.Player1PieceConfigs)
            {
                var pieceModel = new PieceModel(pieceConfig.Id, pieceConfig.Sprite);
                var pieceView = GameObject.Instantiate(m_pieceViewPrefab, m_player1PiecesParent, false);
                pieceView.Initialize(pieceModel);
            }

            foreach (var pieceConfig in m_gameModel.Player2PieceConfigs)
            {
                var pieceModel = new PieceModel(pieceConfig.Id, pieceConfig.Sprite);
                var pieceView = GameObject.Instantiate(m_pieceViewPrefab, m_player2PiecesParent, false);
                pieceView.Initialize(pieceModel);
            }
        }
    }
}
