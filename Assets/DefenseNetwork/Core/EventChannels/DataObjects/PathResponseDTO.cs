using System.Collections.Generic;
using UnityEngine;

namespace DefenseNetwork.Core.EventChannels.DataObjects
{
    public struct PathResponseDTO
    {
        public string RequestID { get; set; } 
        public List<Vector3> PathPoints { get; set; }
    }
}