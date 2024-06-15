﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JGM.Game
{
    public class BoardView : MonoBehaviour
    {
        public Action OnPiecePlaced { get; set; }
        public int PiecesOnBoard => m_boardModel.GetPiecesOnBoard();

        [SerializeField]
        private CellView[] m_cells;

        private BoardModel m_boardModel;
        private BoardController m_boardController;

        public void Initialize(GameModel gameModel, BoardModel boardModel)
        {
            m_boardModel = boardModel;
            InitializeBoardCells(gameModel);
            m_boardController = new BoardController(boardModel, m_cells);
        }

        private void InitializeBoardCells(GameModel gameModel)
        {
            int currentRow = 0;
            int currentColumn = -1;

            for (int i = 0; i < m_cells.Length; i++)
            {
                var boardCellColor = (i % 2 == 0) ? gameModel.BoardCellDarkBrownColor : gameModel.BoardCellLightBrownColor;
                var boardCell = new CellModel(null, boardCellColor, gameModel.BoardCellHighlightedColor);

                if (++currentColumn > 2)
                {
                    currentColumn = 0;
                    currentRow++;
                }

                m_cells[i].Initialize(boardCell, currentRow, currentColumn);
                m_boardModel.SetCell(currentRow, currentColumn, boardCell);
            }
        }

        public void HighlightCellIfValidForPiece(PieceView pieceView, PointerEventData eventData)
        {
            m_boardController.HighlightCellIfValidForPiece(pieceView, eventData);
        }

        public bool PlacePieceOnBoard(PieceView pieceView, PointerEventData eventData)
        {
            bool placedPiece = m_boardController.PlacePieceOnBoard(pieceView, eventData, out var validCell);
            if (placedPiece)
            {
                OnPiecePlaced?.Invoke();
            }
            return placedPiece;
        }

        public bool CheckTicTacToe()
        {
            return m_boardController.CheckTicTacToe();
        }
    }
}
