using System;
using UnityEngine;

namespace DefenseNetwork.Core.EventChannels.DataObjects
{
    public struct EnemySpawnRequestDTO
    {
        public GameObject EnemyToSpawn { get; set; }
        public Action<GameObject> onEnemySpawned { get; set; }
    }
}