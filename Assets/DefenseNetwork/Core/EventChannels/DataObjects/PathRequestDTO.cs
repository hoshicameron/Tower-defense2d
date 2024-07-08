using UnityEngine;

namespace DefenseNetwork.Core.EventChannels.DataObjects
{
    public struct PathRequestDTO
    {
        public string RequestID { get;  set; } 
        public Vector3 StartPosition { get; set; }
        public Vector3 EndPosition { get; set; }
    }
}