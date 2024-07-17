using System.Collections.Generic;
using UnityEngine;

namespace DefenseNetwork.CoreTowerDefense.Requests
{
    public struct PathRequest
    {
        public Vector3 StartPosition { get; set; }
        public Vector3 EndPosition { get; set; }
        
        public delegate void RequestResultHandler(List<Vector3> PathPoints);
        
        public event RequestResultHandler OnRequestResult;
        
        public void InvokeResult(List<Vector3> points)
        {
            OnRequestResult?.Invoke(points);
        }
    }
}