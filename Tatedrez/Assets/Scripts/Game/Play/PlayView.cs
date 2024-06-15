using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class PlayView : ScreenView
    {
        [SerializeField] private AnimatedText m_playerTurnText;
        [SerializeField] private BoardView m_boardView;
        [SerializeField] private PiecesSpawnView[] m_piecesSpawnViews;
        [SerializeField] private MessageView m_messageView;
        [SerializeField] private float m_showWarningSeconds = 3f;
        [SerializeField] private float m_showWinnerSeconds = 1f;
        [Inject] private ICoroutineService m_coroutineService;

        private const int m_minBoardPiecesForTicTacToe = 5;

        private GameView m_gameView;
        private PlayController m_playController;
        private RectTransform m_canvasTransform;
        private BoardModel m_boardModel;

        public override void Initialize(GameView gameView)
        {
            m_gameView = gameView;
            m_playController = new PlayController();
            m_canvasTransform = (RectTransform)gameView.Canvas.transform;

            m_boardModel = new BoardModel(gameView.Model.BoardRows, gameView.Model.BoardColumns);
            m_boardView.Initialize(m_gameView.Model, m_boardModel);
            m_boardView.OnPiecePlaced += OnPiecePlaced;
            InitializePieces();
        }

        private async void OnPiecePlaced()
        {
            if (TicTacToeFound())
            {
                await OnTicTacToeFound();
                return;
            }

            ChangePlayerTurn();
            await CheckIfPlayerCanMove();
        }

        private bool TicTacToeFound()
        {
            bool piecesAmountRequired = (m_boardView.CalculatePiecesAmount() >= m_minBoardPiecesForTicTacToe);
            if (!piecesAmountRequired)
            {
                return false;
            }

            bool ticTacToe = m_boardView.CheckTicTacToe();
            return ticTacToe;
        }

        private async Task OnTicTacToeFound()
        {
            int playerWinId = m_playController.GetPlayerTurn();
            m_boardView.HighlightPlayerWinCells(playerWinId);
            foreach (var piecesSpawnView in m_piecesSpawnViews)
            {
                piecesSpawnView.EnableAllPiecesInteraction();
            }
            m_canvasGroup.blocksRaycasts = false;
            await Task.Delay(TimeSpan.FromSeconds(m_showWinnerSeconds));
            m_gameView.OnPlayerWin(playerWinId);
        }

        private void ChangePlayerTurn()
        {
            m_playController.ChangePlayerTurn();
            SetPlayerTurn();
        }

        private void SetPlayerTurn()
        {
            int playerTurn = m_playController.GetPlayerTurn();
            int nonPlayerTurn = m_playController.GetNonPlayerTurn();

            if (m_boardView.PiecesOnBoard > m_minBoardPiecesForTicTacToe)
            {
                m_piecesSpawnViews[playerTurn].EnableAllPiecesInteraction();
            }
            else
            {
                m_piecesSpawnViews[playerTurn].EnableNonPlacedPiecesInteraction();
            }

            m_playerTurnText.SetIntegerValue(playerTurn + 1);
            m_piecesSpawnViews[nonPlayerTurn].DisableAllPiecesInteraction();
        }

        private async Task CheckIfPlayerCanMove()
        {
            bool allPiecesArePlacedOnBoard = (m_boardView.PiecesOnBoard == m_boardModel.Rows + m_boardModel.Columns);
            if (!allPiecesArePlacedOnBoard)
            {
                return;
            }

            int playerTurn = m_playController.GetPlayerTurn();
            bool playerCannotMove = !m_boardView.AnyPieceFromPlayerCanMove(m_piecesSpawnViews[playerTurn].GetPieces(), m_boardModel);
            if (playerCannotMove)
            {
                await DisplayCannotMoveMessage();
                ChangePlayerTurn();
            }
        }

        private async Task DisplayCannotMoveMessage()
        {
            m_canvasGroup.blocksRaycasts = false;
            int playerTurn = m_playController.GetPlayerTurn();
            m_messageView.ShowMessage(playerTurn + 1);
            await Task.Delay(TimeSpan.FromSeconds(m_showWarningSeconds));
            m_messageView.HideMessage(true);
            m_canvasGroup.blocksRaycasts = true;
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
            m_playController.GenerateFirstTurnRandomly();
            SetPlayerTurn();
            var coroutine = DisablePiecesSpawnLayoutGroups();
            m_coroutineService.StartExternalCoroutine(coroutine);
        }

        private IEnumerator DisablePiecesSpawnLayoutGroups()
        {
            yield return new WaitForEndOfFrame();

            foreach (var spawnView in m_piecesSpawnViews)
            {
                spawnView.DisableLayoutGroup();
            }

            m_messageView.HideMessage(false);
        }
    }
}
