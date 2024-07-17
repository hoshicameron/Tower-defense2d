using System;
using UnityEngine;

namespace DefenseNetwork.CoreTowerDefense.DataRequestObjects
{
    public struct EnemySpawnRequest
    {
        public GameObject EnemyToSpawn { get; set; }
        public Action<GameObject> onEnemySpawned { get; set; }
    }
}