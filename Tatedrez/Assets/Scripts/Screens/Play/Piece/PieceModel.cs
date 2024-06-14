using UnityEngine;

namespace JGM.Game
{
    public class PieceModel
    {
        public string Id { get; }
        public Sprite Sprite { get; }

        public PieceModel(string id, Sprite sprite)
        {
            Id = id;
            Sprite = sprite;
        }
    }
}
