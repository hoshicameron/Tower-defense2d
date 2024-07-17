using System.Collections;
using System.Collections.Generic;
using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using DefenseNetwork.MainSystem.Scripts.ScriptableObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.MainSystem.Scripts
{
    public class WaveManager : MonoBehaviour
    {
        [Header("Event Channel")]
        [SerializeField] private VoidEventChannelSO waveStartEventChannel;
        [SerializeField] private VoidEventChannelSO waveEndEventChannel;
        [SerializeField] private EnemySpawnRequestChannelSO enemySpawnRequestChannel;
        [SerializeField] private GameObjectEventChannelSO enemyDestroyedEventChannel;
        
        [Space]
        [Header("Data")]
        [SerializeField] private WaveDataSO waveData;
        [SerializeField] private float delayBetweenWaves = 2.0f;

        public bool IsLastWave => currentWaveIndex >= waveData.Waves.Count - 1;
        
        private int currentWaveIndex = -1;
        private List<GameObject> activeEnemies = new();
        private Coroutine currentWaveCoroutine;
        private bool isSpawning;
        
        private void OnEnable()
        {
            enemyDestroyedEventChannel.OnEventRaised += HandleEnemyDestroyed;
        }

        private void OnDisable()
        {
            enemyDestroyedEventChannel.OnEventRaised -= HandleEnemyDestroyed;
        }
        
        public void StartNextWave()
        {
            if (IsLastWave)
            {
                waveEndEventChannel.RaiseEvent();
                return;
            }

            currentWaveIndex++;
            waveStartEventChannel.RaiseEvent();
            StartCoroutine(DelayCoRoutine());
        }

        private IEnumerator DelayCoRoutine()
        {
            yield return new WaitForSeconds(delayBetweenWaves);
            currentWaveCoroutine = StartCoroutine(SpawnWave(waveData.Waves[currentWaveIndex]));
        }

        private IEnumerator SpawnWave(WaveDataSO.WaveData wave)
        {
            isSpawning = true;
            foreach (var spawnInfo in wave.SpawnInfos)
            {
                for (var i = 0; i < spawnInfo.Count; i++)
                {
                    enemySpawnRequestChannel.RaiseEvent(new EnemySpawnRequest 
                    { 
                        EnemyToSpawn = spawnInfo.EnemyToSpawn,
                        onEnemySpawned = AddEnemyToActiveList
                    });

                    yield return new WaitForSeconds(spawnInfo.DelayBetweenSpawns);
                }

                yield return new WaitForSeconds(spawnInfo.DelayAfterGroup);
            }
            isSpawning = false;
        }
        
        private void AddEnemyToActiveList(GameObject enemy)
        {
            enemy.name += activeEnemies.Count;
            activeEnemies.Add(enemy);
        }

        private void HandleEnemyDestroyed(GameObject destroyedEnemy)
        {
            activeEnemies.Remove(destroyedEnemy);
        
            if (activeEnemies.Count == 0 && !isSpawning)
            {
                waveEndEventChannel.RaiseEvent();
            
                if (!IsLastWave)
                {
                    StartNextWave();
                }
            }
        }
        
        public void PauseWave()
        {
            if (currentWaveCoroutine == null) return;
            
            StopAllCoroutines();
            currentWaveCoroutine = null;
        }

        public void ResumeWave()
        {
            if (!isSpawning && currentWaveIndex >= 0 && currentWaveIndex < waveData.Waves.Count)
            {
                currentWaveCoroutine = StartCoroutine(SpawnWave(waveData.Waves[currentWaveIndex]));
            }
        }
    }
}