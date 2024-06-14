using UnityEngine;

namespace JGM.Game
{
    public class PieceModel
    {
        public string Id { get; private set; }
        public Sprite Sprite { get; private set; }
        public float EnabledColorAlpha { get; private set; }
        public float DisabledColorAlpha { get; private set; }

        public PieceModel(string id, Sprite sprite, float enabledColorAlpha, float disabledColorAlpha)
        {
            Id = id;
            Sprite = sprite;
            EnabledColorAlpha = enabledColorAlpha;
            DisabledColorAlpha = disabledColorAlpha;
        }
    }
}
