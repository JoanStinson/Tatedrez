using System.Collections;
using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class PlayView : ScreenView
    {
        [SerializeField] private LocalizedText m_playerTurnText;
        [SerializeField] private BoardView m_boardView;
        [SerializeField] private PiecesSpawnView m_leftPiecesSpawnView;
        [SerializeField] private PiecesSpawnView m_rightPiecesSpawnView;

        [Inject] private ICoroutineService m_coroutineService;

        private PlayController m_controller;

        public void Initialize(GameModel gameModel, GameView gameView)
        {
            m_controller = new PlayController();
            m_boardView.Initialize(new BoardModel(3, 3));

            var canvasRect = (RectTransform)gameView.Canvas.transform;
            m_leftPiecesSpawnView.Initialize(gameModel.Player1PieceConfigs, m_boardView.Cells, canvasRect);
            m_rightPiecesSpawnView.Initialize(gameModel.Player2PieceConfigs, m_boardView.Cells, canvasRect);
        }

        public override void Show()
        {
            base.Show();
            m_coroutineService.StartExternalCoroutine(DisablePiecesSpawnLayoutGroups());
        }

        private IEnumerator DisablePiecesSpawnLayoutGroups()
        {
            yield return new WaitForEndOfFrame();
            m_leftPiecesSpawnView.DisableLayoutGroup();
            m_rightPiecesSpawnView.DisableLayoutGroup();
        }
    }
}
