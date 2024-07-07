using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace DefenseNetwork.Modules.AStarPathFinding.Scripts
{
    [Serializable]
    public class Map
    {
        [Header("Grid")]
        [SerializeField] public Grid Grid;
        [Header("Tilemap")]
        [SerializeField] public Tilemap Tilemap;
        [SerializeField] public Tilemap CollisionTilemap;
        [Header("Restriction Tiles")]
        [SerializeField] public TileBase[] UnWalkableTileArray;
        [Header("Tile Visualization")]
        [SerializeField] public Tile StartPathTile;
        [SerializeField] public Tile EndPathTile;
        
        public int[,] AStarMovementPenalty;
        public BoundsInt TilemapBounds => Tilemap.cellBounds;
        public Vector3Int UpperBounds => TilemapBounds.max;
        public Vector3Int LowerBounds => TilemapBounds.min;

        public Tilemap PathTilemap { get; private set; }
        public static readonly Vector3Int InvalidPosition = new(9999, 9999, 9999);

        private const int defaultMovementPenalty = 40;
        
        public void SetupPathTilemap()
        {
            PathTilemap = Object.Instantiate(Tilemap, Grid.transform);
            PathTilemap.GetComponent<TilemapRenderer>().sortingOrder = Tilemap.GetComponent<TilemapRenderer>().sortingOrder +1;
            PathTilemap.gameObject.tag = "Untagged";
        }
        
        public void ClearPath(Stack<Vector3> pathStack)
        {
            if (pathStack == null) return;
            foreach (var worldPosition in pathStack)
            {
                PathTilemap.SetTile(Grid.WorldToCell(worldPosition), null);
            }
        }

        public Vector3Int WorldToCell(Vector3 worldPosition)
        {
            return Grid.WorldToCell(worldPosition);
        }


        public void AddObstacles()
        {
            var width  = UpperBounds.x - LowerBounds.x + 1;
            var height = UpperBounds.y - LowerBounds.y + 1;
            
            AStarMovementPenalty = new int[width, height];

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    AStarMovementPenalty[x, y] = defaultMovementPenalty;

                    if (CollisionTilemap == null) continue;
                    
                    var tile = CollisionTilemap.GetTile(
                        new Vector3Int(x + LowerBounds.x, y + LowerBounds.y, 0));

                    if (UnWalkableTileArray.Any(collisionTile => tile == collisionTile))
                    {
                        AStarMovementPenalty[x, y] = 0;
                    }
                }
            }
        }
        
        public bool IsPositionWithinBounds(Vector3Int position)
        {
            return position.x >= LowerBounds.x &&
                   position.x <= UpperBounds.x &&
                   position.y >= LowerBounds.y &&
                   position.y <= UpperBounds.y;
        }
    }
}