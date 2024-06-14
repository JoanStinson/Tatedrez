using System.Collections;
using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class PlayView : ScreenView
    {
        [SerializeField] private LocalizedText m_playerTurnText;
        [SerializeField] private BoardView m_boardView;
        [SerializeField] private PiecesSpawnView[] m_piecesSpawnViews;

        [Inject] private ICoroutineService m_coroutineService;

        private PlayController m_playController;

        public void Initialize(GameModel gameModel, GameView gameView)
        {
            m_playController = new PlayController(gameModel);
            m_boardView.Initialize(gameModel, new BoardModel(3, 3));
            m_boardView.OnPiecePlaced += OnPiecePlaced;

            var canvasRect = (RectTransform)gameView.Canvas.transform;
            for (int i = 0; i < m_piecesSpawnViews.Length; i++)
            {
                m_piecesSpawnViews[i].Initialize(gameModel, i, m_boardView, canvasRect);
            }
        }

        private void OnPiecePlaced()
        {
            int playerTurn = m_playController.ChangePlayerTurn();
            SetPlayerTurn(playerTurn, playerTurn ^ 1);
        }

        private void SetPlayerTurn(int playerTurn, int nonPlayerTurn)
        {
            m_playerTurnText.SetIntegerValue(playerTurn + 1);
            m_piecesSpawnViews[playerTurn].EnableInteraction();
            m_piecesSpawnViews[nonPlayerTurn].DisableInteraction();
        }

        public override void Show()
        {
            base.Show();
            int playerTurn = m_playController.StartNewGame();
            SetPlayerTurn(playerTurn, playerTurn ^ 1);
            m_coroutineService.StartExternalCoroutine(DisablePiecesSpawnLayoutGroups());
        }

        private IEnumerator DisablePiecesSpawnLayoutGroups()
        {
            yield return new WaitForEndOfFrame();
            foreach (var spawnView in m_piecesSpawnViews)
            {
                spawnView.DisableLayoutGroup();
            }
        }
    }
}
