using System.Collections.Generic;
using DefenseNetwork.AStarPathFinding.Scripts;
using UnityEngine;

namespace DefenseNetwork.Modules.AStarPathFinding.Scripts
{
    public static class AStar
    {
        public static Stack<Vector3> BuildPath(Map map,Vector3Int startGridPosition, Vector3Int targetGridPosition)
        {
            startGridPosition -= map.LowerBounds;
            targetGridPosition -= map.LowerBounds;
            
            var openNodeList = new List<Node>();
            var closedNodesHashSet = new HashSet<Node>();

            var gridNodes = new GridNodes(map.UpperBounds.x - map.LowerBounds.x + 1,
                map.UpperBounds.y - map.LowerBounds.y + 1);

            var startNode = gridNodes.GetGridNode(startGridPosition.x, startGridPosition.y);
            var targetNode = gridNodes.GetGridNode(targetGridPosition.x, targetGridPosition.y);

            var endPathNode = FindShortestPath(startNode, targetNode, gridNodes, openNodeList, closedNodesHashSet, map);

            return endPathNode != null ? CreatePathStack(endPathNode, map) : null;
        }

        private static Stack<Vector3> CreatePathStack(Node targetNode, Map map)
        {
            var movementPathStack = new Stack<Vector3>();
            var nextNode = targetNode;
            var cellMidPoint = map.Grid.cellSize * 0.5f;
            cellMidPoint.z = 0f;

            while (nextNode != null)
            {
                var worldPosition = map.Tilemap.CellToWorld(
                    new Vector3Int(nextNode.gridPosition.x + map.LowerBounds.x,
                                            nextNode.gridPosition.y + map.LowerBounds.y, 0));

                worldPosition += cellMidPoint;
                movementPathStack.Push(worldPosition);
                nextNode = nextNode.parentNode;
            }

            return movementPathStack; 
        }

        private static Node FindShortestPath(Node startNode, Node targetNode, GridNodes gridNodes, List<Node> openNodeList,
            HashSet<Node> closedNodesHashSet, Map map)
        {
            openNodeList.Add(startNode);

            while (openNodeList.Count >0 )
            {
                openNodeList.Sort();

                var currentNode = openNodeList[0];
                openNodeList.RemoveAt(0);

                if (currentNode == targetNode)
                    return currentNode;

                closedNodesHashSet.Add(currentNode);

                EvaluateCurrentNodeNeighbours(currentNode, targetNode, gridNodes, openNodeList, closedNodesHashSet,
                    map);
            }

            return null;
        }

        private static void EvaluateCurrentNodeNeighbours(Node currentNode, Node targetNode, GridNodes gridNodes, List<Node> openNodeList,
            HashSet<Node> closedNodesHashSet, Map map)
        {
            var currentNodeGridPosition = currentNode.gridPosition;

            Node validNeighbourNode;
            for (var i = -1; i <=  1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    if(i == 0 && j == 0)
                        continue;

                    validNeighbourNode = GetValidNodeNeighbour(currentNodeGridPosition.x + i,
                        currentNodeGridPosition.y + j, gridNodes, closedNodesHashSet, map);

                    if (validNeighbourNode == null) continue;

                    var movementPenaltyForGridSpace = map.AStarMovementPenalty[validNeighbourNode.gridPosition.x,
                        validNeighbourNode.gridPosition.y];
                    
                    var newCostToNeighbour = currentNode.GCost + GetDistance(currentNode, validNeighbourNode)
                        + movementPenaltyForGridSpace;
                    

                    var isValidNeighbourInOpenList = openNodeList.Contains(validNeighbourNode);

                    if (newCostToNeighbour >= validNeighbourNode.GCost && isValidNeighbourInOpenList) continue;
                    
                    validNeighbourNode.GCost = newCostToNeighbour;
                    validNeighbourNode.HCost = GetDistance(validNeighbourNode, targetNode);
                    validNeighbourNode.parentNode = currentNode;
                            
                    if (!isValidNeighbourInOpenList)
                        openNodeList.Add(validNeighbourNode);
                }
            }
        }

        private static int GetDistance(Node nodeA, Node nodeB)
        {
            var dstX = Mathf.Abs(nodeA.gridPosition.x - nodeB.gridPosition.x);
            var dstY = Mathf.Abs(nodeA.gridPosition.y - nodeB.gridPosition.y);

            return dstX > dstY ? 14 * dstY + 10 * (dstX - dstY) : 14 * dstX + 10 * (dstY - dstX);
        }

        private static Node GetValidNodeNeighbour(int neighbourNodeXPosition, int neighbourNodeYPosition, GridNodes gridNodes, HashSet<Node> closedNodesHashSet, Map map)
        {
            if (neighbourNodeXPosition >= map.UpperBounds.x - map.LowerBounds.x || neighbourNodeXPosition < 0
                || neighbourNodeYPosition >= map.UpperBounds.y - map.LowerBounds.y || neighbourNodeYPosition < 0)
                return null;
            
            var neighbourNode = gridNodes.GetGridNode(neighbourNodeXPosition, neighbourNodeYPosition);
            
            var movementPenaltyForGridSpace = map.AStarMovementPenalty[neighbourNodeXPosition,neighbourNodeYPosition];

            return (closedNodesHashSet.Contains(neighbourNode) || movementPenaltyForGridSpace == 0) ? null : neighbourNode;
        }
    }
}