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

        private GameView m_gameView;
        private PlayController m_playController;
        private BoardModel m_boardModel;

        public void Initialize(GameModel gameModel, GameView gameView)
        {
            m_gameView = gameView;
            m_playController = new PlayController(gameModel);
            m_boardModel = new BoardModel(3, 3);
            m_boardView.Initialize(gameModel, m_boardModel);
            m_boardView.OnPiecePlaced += OnPiecePlaced;

            var canvasTransform = (RectTransform)gameView.Canvas.transform;
            for (int i = 0; i < m_piecesSpawnViews.Length; i++)
            {
                m_piecesSpawnViews[i].Initialize(gameModel, i, m_boardView, canvasTransform);
            }
        }

        private void OnPiecePlaced()
        {
            if (m_boardView.PiecesOnBoard < m_boardModel.Rows + m_boardModel.Columns)
            {
                int playerTurn = m_playController.ChangePlayerTurn();
                m_playerTurnText.SetIntegerValue(playerTurn + 1);
                m_piecesSpawnViews[playerTurn].EnableNonPlacedPiecesInteraction();
                m_piecesSpawnViews[playerTurn ^ 1].DisableAllPiecesInteraction();
                return;
            }

            bool ticTacToe = m_boardView.CheckTicTacToe();
            if (ticTacToe)
            {
                m_gameView.OnPlayerWin();
                return;
            }

            int turn = m_playController.ChangePlayerTurn();
            m_playerTurnText.SetIntegerValue(turn + 1);
            m_piecesSpawnViews[turn].EnableAllPiecesInteraction();
            m_piecesSpawnViews[turn ^ 1].DisableAllPiecesInteraction();
        }

        private void SetPlayerTurn(int playerTurn, int nonPlayerTurn)
        {
            m_playerTurnText.SetIntegerValue(playerTurn + 1);
            m_piecesSpawnViews[playerTurn].EnableAllPiecesInteraction();
            m_piecesSpawnViews[nonPlayerTurn].DisableAllPiecesInteraction();
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
