using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JGM.Game
{
    public class PiecesSpawnView : MonoBehaviour
    {
        [SerializeField] private VerticalLayoutGroup m_piecesSpawnParent;
        [SerializeField] private PieceView m_pieceViewPrefab;
        [Inject] private ICoroutineService m_coroutineService;

        public void Initialize(GameSettings.PieceConfig[] pieceConfigs, CellView[] boardCells, RectTransform canvasRect)
        {
            InstantiatePieces(pieceConfigs, boardCells, canvasRect);
            DisableLayoutGroups();
        }

        private void InstantiatePieces(GameSettings.PieceConfig[] pieceConfigs, CellView[] boardCells, RectTransform canvasRect)
        {
            m_piecesSpawnParent.transform.DestroyAllChildren();

            foreach (var pieceConfig in pieceConfigs)
            {
                var pieceModel = new PieceModel(pieceConfig.Id, pieceConfig.Sprite);
                var pieceView = Instantiate(m_pieceViewPrefab, m_piecesSpawnParent.transform, false);
                pieceView.Initialize(pieceModel, boardCells, canvasRect);
                pieceView.gameObject.SetName(pieceConfig.Id);
            }
        }

        private void DisableLayoutGroups()
        {
            m_coroutineService.DelayedCallByFrames(() =>
            {
                m_piecesSpawnParent.enabled = false;
            }, 1);
        }
    }
}
