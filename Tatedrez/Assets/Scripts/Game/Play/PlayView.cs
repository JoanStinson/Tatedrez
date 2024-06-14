using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JGM.Game
{
    public class PlayView : ScreenView
    {
        [SerializeField] private LocalizedText m_playerTurnText;
        [SerializeField] private VerticalLayoutGroup m_player1PiecesParent;
        [SerializeField] private VerticalLayoutGroup m_player2PiecesParent;
        [SerializeField] private PieceView m_pieceViewPrefab;
        [SerializeField] private SlotView[] m_slotViews;
        [SerializeField] private RectTransform m_canvasRect;

        [Inject] private ICoroutineService m_coroutineService;
        private GameView m_gameView;
        private GameModel m_gameModel;
        private PlayController m_controller;

        public void Initialize(GameView gameView, GameModel gameModel, PlayController controller)
        {
            m_gameView = gameView;
            m_gameModel = gameModel;
            m_controller = controller;

            m_player1PiecesParent.transform.DestroyAllChildren();
            m_player2PiecesParent.transform.DestroyAllChildren();

            SpawnPieces(m_gameModel.Player1PieceConfigs, m_player1PiecesParent.transform);
            SpawnPieces(m_gameModel.Player2PieceConfigs, m_player2PiecesParent.transform);

            m_coroutineService.DelayedCallByFrames(() =>
            {
                m_player1PiecesParent.enabled = false;
                m_player2PiecesParent.enabled = false;
            }, 1);
        }

        private void SpawnPieces(GameSettings.PieceConfig[] pieceConfigs, Transform parent)
        {
            foreach (var pieceConfig in pieceConfigs)
            {
                var pieceModel = new PieceModel(pieceConfig.Id, pieceConfig.Sprite);
                var pieceView = GameObject.Instantiate(m_pieceViewPrefab, parent, false);
                pieceView.gameObject.SetName(pieceConfig.Id);
                pieceView.Initialize(pieceModel, m_canvasRect, m_slotViews);
            }
        }
    }
}
