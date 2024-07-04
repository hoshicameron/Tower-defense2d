using System;
using UnityEngine;

namespace DefenseNetwork.AStarPathFinding.Scripts
{
    public class Node : IComparable<Node>
    {
        public Vector2Int gridPosition;
        public int GCost = 0;
        public int HCost = 0;
        public Node parentNode;

        public Node(Vector2Int gridPosition)
        {
            this.gridPosition = gridPosition;
            parentNode = null;
        }

        public int FCost => GCost + HCost;
        
        
        public int CompareTo(Node nodeToCompare)
        {
            var compare = FCost.CompareTo(nodeToCompare.FCost);
            
            if (compare == 0)
                compare = HCost.CompareTo(nodeToCompare.HCost);
            
            return compare;
        }
    }
}