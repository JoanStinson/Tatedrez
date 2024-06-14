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

            m_player1PiecesParent.DestroyAllChildren();
            SpawnPieces(m_gameModel.Player1PieceConfigs, m_player1PiecesParent);
            
            m_player2PiecesParent.DestroyAllChildren();
            SpawnPieces(m_gameModel.Player2PieceConfigs, m_player2PiecesParent);
        }

        private void SpawnPieces(GameSettings.PieceConfig[] pieceConfigs, Transform parent)
        {
            foreach (var pieceConfig in pieceConfigs)
            {
                var pieceModel = new PieceModel(pieceConfig.Id, pieceConfig.Sprite);
                var pieceView = GameObject.Instantiate(m_pieceViewPrefab, parent, false);
                pieceView.Initialize(pieceModel);
            }
        }
    }
}
