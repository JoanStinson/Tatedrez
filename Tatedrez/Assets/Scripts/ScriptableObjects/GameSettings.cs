using System;
using UnityEngine;

namespace JGM.Game
{
    [CreateAssetMenu(fileName = "New Game Settings", menuName = "Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [Header("Player Pieces")]
        [SerializeField] private PieceConfig[] m_player1PieceConfigs;
        [SerializeField] private PieceConfig[] m_player2PieceConfigs;

        [Serializable]
        public class PieceConfig
        {
            public string Id;
            public Sprite Sprite;
        }

        public PieceConfig[] Player1PieceConfigs => m_player1PieceConfigs;
        public PieceConfig[] Player2PieceConfigs => m_player2PieceConfigs;
    }
}