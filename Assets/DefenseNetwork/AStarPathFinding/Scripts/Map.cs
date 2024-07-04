using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefenseNetwork.AStarPathFinding.Scripts
{
    [Serializable]
    public class Map
    {
        [SerializeField] public Grid Grid;
        [SerializeField] public Tilemap Tilemap;
        [SerializeField] public Tilemap CollisionTilemap;
        [SerializeField] public TileBase[] UnWalkableTileArray;
            
        public int[,] AStarMovementPenalty;
        public BoundsInt TilemapBounds => Tilemap.cellBounds;
        public Vector3Int UpperBounds => TilemapBounds.max;
        public Vector3Int LowerBounds => TilemapBounds.min;
    }
}