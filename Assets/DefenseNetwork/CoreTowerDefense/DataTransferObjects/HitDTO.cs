using UnityEngine;

namespace DefenseNetwork.CoreTowerDefense.DataTransferObjects
{
    public struct HitDTO
    {
        public GameObject HittedObject { get; set; }
        public int Damage { get; set; }
    }
}