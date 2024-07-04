using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefenseNetwork.AStarPathFinding.Scripts
{
    public partial class AStarTest : MonoBehaviour
    {
        [SerializeField] private Map map;
        [SerializeField] private Tile startPathTile;
        [SerializeField] private Tile endPathTile;
         
        
        private Node startNode;
        private Node endNode;
        private Camera mainCamera;
        private Tilemap pathTilemap;

        private Vector3Int startGridPosition;
        private Vector3Int endGridPosition;

        private readonly Vector3Int noValue = new(9999, 9999, 9999);
        
        private Stack<Vector3> pathStack;
        private const int defaultMovementPenalty = 40;

        private void Start()
        {
            mainCamera = Camera.main;
            pathStack = null;
            startGridPosition = noValue;
            endGridPosition = noValue;
            
            AddObstacles();
            SetupPathTilemap();
        }

        private void AddObstacles()
        {
            var width  = map.UpperBounds.x - map.LowerBounds.x + 1;
            var height = map.UpperBounds.y - map.LowerBounds.y + 1;
            
            map.AStarMovementPenalty = new int[width, height];

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    map.AStarMovementPenalty[x, y] = defaultMovementPenalty;

                    var tile = map.CollisionTilemap.GetTile(
                        new Vector3Int(x + map.LowerBounds.x, y + map.LowerBounds.y, 0));

                    if (map.UnWalkableTileArray.Any(collisionTile => tile == collisionTile))
                    {
                        map.AStarMovementPenalty[x, y] = 0;
                    }
                }
            }
        }

        private void SetupPathTilemap()
        {
            pathTilemap = Instantiate(map.Tilemap, map.Grid.transform);
            pathTilemap.GetComponent<TilemapRenderer>().sortingOrder = 2;
            pathTilemap.gameObject.tag = "Untagged";
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("S");
                ClearPath();
                SetStartNode();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                ClearPath();
                SetEndPosition();
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                DisplayPath();
            }
        }

        private void DisplayPath()
        {
            
            if(startGridPosition == noValue || endGridPosition == noValue)  return;

            pathStack = AStar.BuildPath(map, startGridPosition, endGridPosition);
            
            if(pathStack == null)   return;

            foreach (var worldPosition in pathStack)
            {
                pathTilemap.SetTile(map.Grid.WorldToCell(worldPosition), startPathTile);
            }
        }

        private void SetEndPosition()
        {
            if (endGridPosition == noValue)
            {
                endGridPosition = map.Grid.WorldToCell(GetMouseWorldPosition());
                if (!IsPositionWithinBounds(endGridPosition))
                {
                    endGridPosition = noValue;
                    return;
                }
                pathTilemap.SetTile(endGridPosition , startPathTile);
            }
            else
            {
                pathTilemap.SetTile(endGridPosition, null);
                endGridPosition = noValue;
            }
        }

        private void SetStartNode()
        {
            if (startGridPosition == noValue)
            {
                startGridPosition = map.Grid.WorldToCell(GetMouseWorldPosition());
                if (!IsPositionWithinBounds(startGridPosition))
                {
                    startGridPosition = noValue;
                    return;
                }
                pathTilemap.SetTile(startGridPosition , endPathTile);
            }
            else
            {
                pathTilemap.SetTile(startGridPosition, null);
                startGridPosition = noValue;
            }
        }

        private bool IsPositionWithinBounds(Vector3Int position)
        {
            return position.x >= map.LowerBounds.x &&
                   position.x <= map.UpperBounds.x &&
                   position.y >= map.LowerBounds.y &&
                   position.y <= map.UpperBounds.y;
        }

        private void ClearPath()
        {
            if (pathStack == null) return;
            foreach (var worldPosition in pathStack)
            {
                pathTilemap.SetTile(map.Grid.WorldToCell(worldPosition), null);
            }

            pathStack = null;
            endGridPosition = startGridPosition = noValue;
        }

        private Vector3 GetMouseWorldPosition()
        {
            var mousePosition = Input.mousePosition;
            return mainCamera.ScreenToWorldPoint(mousePosition);
        }
    }
}