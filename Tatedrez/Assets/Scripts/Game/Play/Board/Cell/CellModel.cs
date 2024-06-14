using UnityEngine;

namespace JGM.Game
{
    public class CellModel
    {
        public string PieceType { get; private set; }
        public Color DefaultColor { get; private set; }
        public Color HighlightedColor { get; private set; }

        public CellModel(string pieceType, Color defaultColor, Color highlightedColor)
        {
            PieceType = pieceType;
            DefaultColor = defaultColor;
            HighlightedColor = highlightedColor;
        }

        public bool IsEmpty()
        {
            return PieceType != null;
        }
    }
}
