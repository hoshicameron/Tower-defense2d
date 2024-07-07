using System.Collections.Generic;
using DefenseNetwork.AStarPathFinding.Scripts;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefenseNetwork.Modules.AStarPathFinding.Scripts
{
    public class AStarPathBuilder
    {
        private readonly Map map;
        private Vector3Int startGridPosition;
        private Vector3Int endGridPosition;
        private Stack<Vector3> pathStack;

        public AStarPathBuilder(Map map)
        {
            this.map = map;
            this.map.AddObstacles();
            ResetPositions();
        }

        public void ResetPositions()
        {
            startGridPosition = Map.InvalidPosition;
            endGridPosition = Map.InvalidPosition;
            pathStack = null;
        }

        public void SetStartPosition(Vector3 worldPosition)
        {
            SetPosition(ref startGridPosition, worldPosition, map.StartPathTile);
        }

        public void SetEndPosition(Vector3 worldPosition)
        {
            SetPosition(ref endGridPosition, worldPosition, map.EndPathTile);
        }

        private void SetPosition(ref Vector3Int gridPosition, Vector3 worldPosition, Tile tile)
        {
            if (gridPosition == Map.InvalidPosition)
            {
                gridPosition = map.WorldToCell(worldPosition);
                if (!map.IsPositionWithinBounds(gridPosition))
                {
                    gridPosition = Map.InvalidPosition;
                    return;
                }

                if (tile != null) map.PathTilemap.SetTile(gridPosition, tile);
            }
            else
            {
                map.PathTilemap.SetTile(gridPosition, null);
                gridPosition = Map.InvalidPosition;
            }
        }

        public Stack<Vector3> CreatePath()
        {
            if (startGridPosition == Map.InvalidPosition || endGridPosition == Map.InvalidPosition)
                return null;

            return AStar.BuildPath(map, startGridPosition, endGridPosition);
        }

        public void VisualizePath()
        {
            pathStack = CreatePath();
            if (pathStack == null) return;

            foreach (var worldPosition in pathStack)
            {
                map.PathTilemap.SetTile(map.WorldToCell(worldPosition), map.StartPathTile);
            }
        }

        public void ClearPath()
        {
            if (pathStack == null) return;
            foreach (var worldPosition in pathStack)
            {
                map.PathTilemap.SetTile(map.WorldToCell(worldPosition), null);
            }
            ResetPositions();
        }
    }
}