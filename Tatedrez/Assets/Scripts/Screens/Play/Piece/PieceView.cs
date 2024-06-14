using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class PieceView : MonoBehaviour
    {
        public string Id { get; private set; }
        
        [SerializeField] private Image m_image;

        public void Initialize(PieceModel pieceModel)
        {
            Id = pieceModel.Id;
            m_image.sprite = pieceModel.Sprite;
        }
    }
}
