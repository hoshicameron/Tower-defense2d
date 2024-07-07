using UnityEngine;

namespace DefenseNetwork.Core.EventChannels.DataObjects
{
    public struct PathRequestDO
    {
        public string RequestID { get;  set; } 
        public Vector3 StartPosition { get; set; }
        public Vector3 EndPosition { get; set; }
    }
}