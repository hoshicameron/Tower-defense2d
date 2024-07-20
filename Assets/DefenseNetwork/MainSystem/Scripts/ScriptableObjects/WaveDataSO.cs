using System.Collections.Generic;
using UnityEngine;

namespace DefenseNetwork.MainSystem.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New WaveDataSO", menuName = "Gameplay/Data/WaveDataSO", order = 0)]
    public class WaveDataSO : ScriptableObject
    {
        [field:SerializeField] public float DelayBetweenWaves { get; private set; } = 4.0f;
        [field:SerializeField] public List<WaveData> Waves { get; set; }

        public int TotalWaves => Waves.Count;

        [System.Serializable]
        public class WaveData
        {
            [field:SerializeField] public List<SpawnInfo> SpawnInfos { get; set; }
        }

        [System.Serializable]
        public struct SpawnInfo
        {
            [field:SerializeField] public GameObject EnemyToSpawn { get; set; }
            [field: SerializeField] public int Count { get; set; }
            [field: SerializeField] public float DelayBetweenSpawns { get; set; }
            [field: SerializeField] public float DelayAfterGroup { get; set; }
        }

    }
}