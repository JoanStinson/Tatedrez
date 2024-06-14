using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JGM.Game
{
    public class PiecesSpawnView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private VerticalLayoutGroup m_piecesSpawnParent;
        [SerializeField] private PieceView m_pieceViewPrefab;
        [Inject] private ICoroutineService m_coroutineService;

        private readonly List<PieceView> m_pieceViewInstances = new List<PieceView>();

        public void Initialize(GameModel gameModel, int playerIndex, BoardView boardView, RectTransform canvasRect)
        {
            InstantiatePieces(gameModel, playerIndex, boardView, canvasRect);
        }

        private void InstantiatePieces(GameModel gameModel, int playerIndex, BoardView boardView, RectTransform canvasRect)
        {
            m_piecesSpawnParent.transform.DestroyAllChildren();

            foreach (var pieceConfig in gameModel.GetPieceConfigs(playerIndex))
            {
                var pieceModel = new PieceModel(pieceConfig.Id, pieceConfig.Sprite, gameModel.PieceEnabledColorAlpha, gameModel.PieceDisabledColorAlpha);
                var pieceView = Instantiate(m_pieceViewPrefab, m_piecesSpawnParent.transform, false);
                pieceView.Initialize(pieceModel, boardView, canvasRect);
                pieceView.gameObject.SetName(pieceConfig.Id);
                m_pieceViewInstances.Add(pieceView);
            }
        }

        public void DisableLayoutGroup()
        {
            m_piecesSpawnParent.enabled = false;
        }

        public void EnableInteraction()
        {
            m_canvasGroup.blocksRaycasts = true;

            foreach (var instance in m_pieceViewInstances)
            {
                instance.SetEnabledColorAlpha();
            }
        }

        public void DisableInteraction()
        {
            m_canvasGroup.blocksRaycasts = false;

            foreach (var instance in m_pieceViewInstances)
            {
                instance.SetDisabledColorAlpha();
            }
        }
    }
}
