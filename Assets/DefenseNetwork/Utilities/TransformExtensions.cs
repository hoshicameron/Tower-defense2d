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
        
        public static Transform GetHighestParent(this Transform transform)
        {
            var highestParent = transform;
        
            while (highestParent.parent != null)
            {
                highestParent = highestParent.parent;
            }
        
            return highestParent;
        }
        
        public static Vector2 GetWorldPositionFromHighestParent(this Transform transform)
        {
            var highestParent = transform;

            while (highestParent.parent != null)
            {
                highestParent = highestParent.parent;
            }

            return highestParent.TransformPoint(transform.position);
        }
    }
}