using System.Diagnostics;
using UnityEngine;

namespace JGM.Game
{
    public static class MonoBehaviourExtensions
    {
        [Conditional("UNITY_EDITOR")]
        public static void SetName(this GameObject gameObject, string name)
        {
            gameObject.name = name;
        }

        public static void DestroyAllChildren(this Transform parentTransform)
        {
            foreach (Transform child in parentTransform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}