using UnityEngine;

namespace JGM.Game
{
    public class CellModel
    {
        public PieceModel PieceModel { get; private set; }
        public Color DefaultColor { get; private set; }
        public Color HighlightedColor { get; private set; }

        public CellModel(PieceModel pieceModel, Color defaultColor, Color highlightedColor)
        {
            PieceModel = pieceModel;
            DefaultColor = defaultColor;
            HighlightedColor = highlightedColor;
        }

        public void SetPieceModel(PieceModel pieceModel)
        {
            PieceModel = pieceModel;
        }

        public void RemovePieceModel()
        {
            PieceModel = null;
        }
    }
}
