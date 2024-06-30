using UnityEngine;

namespace Utilities
{
    public static class TransformExtensions
    {
        public static void DestroyAllChildren(this RectTransform rectTransform)
        {
            for (var i = rectTransform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(rectTransform.GetChild(i).gameObject);
            }
        }
    }
}