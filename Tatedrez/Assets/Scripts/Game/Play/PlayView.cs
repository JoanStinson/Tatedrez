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

        private const int m_piecesForTicTacToe = 5;

        private GameView m_gameView;
        private PlayController m_playController;

        public override void Initialize(GameView gameView)
        {
            m_gameView = gameView;
            m_playController = new PlayController(m_gameView.Model);
            var boardModel = new BoardModel(gameView.Model.BoardRows, gameView.Model.BoardColumns);
            m_boardView.Initialize(m_gameView.Model, boardModel);
            m_boardView.OnPiecePlaced += OnPiecePlaced;

            var canvasTransform = (RectTransform)gameView.Canvas.transform;
            for (int i = 0; i < m_piecesSpawnViews.Length; i++)
            {
                m_piecesSpawnViews[i].Initialize(m_gameView.Model, i, m_boardView, canvasTransform);
            }
        }

        private void OnPiecePlaced()
        {
            if (m_boardView.PiecesOnBoard >= m_piecesForTicTacToe && m_boardView.CheckTicTacToe())
            {
                m_gameView.OnPlayerWin();
                return;
            }

            int playerTurn = m_playController.ChangePlayerTurn();
            SetPlayerTurn(playerTurn, playerTurn ^ 1, m_boardView.PiecesOnBoard > m_piecesForTicTacToe);
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
