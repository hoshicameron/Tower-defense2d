using UnityEngine;

namespace DefenseNetwork.Core.EventChannels.DataObjects
{
    public struct HitDTO
    {
        public GameObject HittedObject { get; set; }
        public int Damage { get; set; }
    }
}