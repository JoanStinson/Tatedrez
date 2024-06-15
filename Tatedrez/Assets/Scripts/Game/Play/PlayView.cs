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
            bool allPiecesPlaced = (m_boardView.PiecesOnBoard == m_boardModel.Rows + m_boardModel.Columns);
            if (allPiecesPlaced)
            {
                bool ticTacToe = m_boardView.CheckTicTacToe();
                if (ticTacToe)
                {
                    m_gameView.OnPlayerWin();
                    return;
                }

                int newTurn = m_playController.ChangePlayerTurn();
                SetPlayerTurn(newTurn, newTurn ^ 1, true);
                return;
            }

            int playerTurn = m_playController.ChangePlayerTurn();
            SetPlayerTurn(playerTurn, playerTurn ^ 1, false);
        }

        private void SetPlayerTurn(int playerTurn, int nonPlayerTurn, bool enableAllPieces)
        {
            m_playerTurnText.SetIntegerValue(playerTurn + 1);

            if (enableAllPieces)
            {
                m_piecesSpawnViews[playerTurn].EnableAllPiecesInteraction();
            }
            else
            {
                m_piecesSpawnViews[playerTurn].EnableNonPlacedPiecesInteraction();
            }

            m_piecesSpawnViews[nonPlayerTurn].DisableAllPiecesInteraction();
        }

        public override void Show()
        {
            base.Show();
            int playerTurn = m_playController.StartNewGame();
            SetPlayerTurn(playerTurn, playerTurn ^ 1, true);
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
