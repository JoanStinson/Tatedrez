using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class PlayView : ScreenView
    {
        [SerializeField] private LocalizedText m_playerTurnText;
        [SerializeField] private BoardView m_boardView;
        [SerializeField] private PiecesSpawnView[] m_piecesSpawnViews;
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private float m_showWinnerSeconds = 1f;
        [Inject] private ICoroutineService m_coroutineService;

        private const int m_piecesForTicTacToe = 5;

        private GameView m_gameView;
        private PlayController m_playController;
        private RectTransform m_canvasTransform;

        public override void Initialize(GameView gameView)
        {
            m_gameView = gameView;
            m_playController = new PlayController(m_gameView.Model);
            m_canvasTransform = (RectTransform)gameView.Canvas.transform;

            var boardModel = new BoardModel(gameView.Model.BoardRows, gameView.Model.BoardColumns);
            m_boardView.Initialize(m_gameView.Model, boardModel);
            m_boardView.OnPiecePlaced += OnPiecePlaced;
            InitializePieces();
        }

        private async void OnPiecePlaced()
        {
            if (m_boardView.PiecesOnBoard >= m_piecesForTicTacToe && m_boardView.CheckTicTacToe())
            {
                int playerWinId = m_playController.GetPlayerTurn();
                m_boardView.HighlightPlayerWinCells(playerWinId);
                m_canvasGroup.blocksRaycasts = false;
                EnableAllPieces();

                await Task.Delay(TimeSpan.FromSeconds(m_showWinnerSeconds));
                m_gameView.OnPlayerWin(playerWinId);
                return;
            }

            int playerTurn = m_playController.ChangePlayerTurn();
            SetPlayerTurn(playerTurn, playerTurn ^ 1, m_boardView.PiecesOnBoard > m_piecesForTicTacToe);
        }

        private void EnableAllPieces()
        {
            foreach (var piecesSpawnView in m_piecesSpawnViews)
            {
                piecesSpawnView.EnableAllPiecesInteraction();
            }
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

        private void InitializePieces()
        {
            for (int i = 0; i < m_piecesSpawnViews.Length; i++)
            {
                m_piecesSpawnViews[i].Initialize(m_gameView.Model, i, m_boardView, m_canvasTransform);
            }
        }

        public override void Show()
        {
            base.Show();
            m_canvasGroup.blocksRaycasts = true;

            m_boardView.ClearBoard();
            InitializePieces();

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
