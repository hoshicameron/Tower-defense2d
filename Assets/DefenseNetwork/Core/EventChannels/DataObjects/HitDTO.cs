using UnityEngine;

namespace DefenseNetwork.Core.EventChannels.DataObjects
{
    public struct HitDTO
    {
        public GameObject hittedObject;
        public int damage;
    }
}