using UnityEngine;
using UnityEngine.Tilemaps;

namespace Utilities
{
    public class TilemapBoundsHelper
    {
        public static BoundsInt GetTilemapBounds(Tilemap tilemap)
        {
            if (tilemap != null) return tilemap.cellBounds;
            
            Debug.LogError("Tilemap reference is null!");
            return new BoundsInt(); // Return empty BoundsInt if no tilemap provided

        }
    }
}