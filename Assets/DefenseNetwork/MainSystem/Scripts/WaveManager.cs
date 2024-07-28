using System;
using System.Collections;
using DefenseNetwork.CoreTowerDefense.Enums;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using DefenseNetwork.MainSystem.Scripts.ScriptableObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.MainSystem.Scripts
{
    public class WaveManager : MonoBehaviour
    {
        [Header("Event Channel")]
        [SerializeField] private VoidEventChannelSO waveEndEventChannel;
        [SerializeField] private GameObjectEventChannelSO enemySpawnChannel;
        [SerializeField] private StringEventChannelSO waveProgressEventChannel;
        [SerializeField] private GameStateEventChannelSO gameStateEventChannel;
        [SerializeField] private VoidEventChannelSO startGameChannel;
        [SerializeField] private VoidEventChannelSO enemyGameObjectRuntimeSetBecameEmptyChannel;
        
        [Space]
        [Header("Data")]
        [SerializeField] private WaveDataSO waveData;
        private bool IsLastWave => currentWaveIndex >= waveData.Waves.Count - 1;
        
        private int currentSpawnInfoIndex;
        private int currentEnemyIndex;
        private int currentWaveIndex;
        
        private Coroutine currentWaveCoroutine;
        
        private bool isSpawning;
        
        private void OnEnable()
        {
            Initialize();
            
            gameStateEventChannel.OnEventRaised += HandleGameStateChange;
            startGameChannel.OnEventRaised += StartSpawnEnemies;
            enemyGameObjectRuntimeSetBecameEmptyChannel.OnEventRaised += CurrentWaveSpawnedEnemyDestroyed;
        }
        private void OnDisable()
        {
            gameStateEventChannel.OnEventRaised -= HandleGameStateChange;
            startGameChannel.OnEventRaised -= StartSpawnEnemies;
            enemyGameObjectRuntimeSetBecameEmptyChannel.OnEventRaised -= CurrentWaveSpawnedEnemyDestroyed;
        }
        
        private void StartSpawnEnemies()
        {
            StartNextWave();
        }
        
        private void HandleGameStateChange(GameState state)
        {
            switch (state)
            {
                case GameState.Playing:
                    ResumeWave();
                    break;
                case GameState.Paused:
                case GameState.Won:
                case GameState.Lost:
                    PauseWave();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
        
        private void Initialize()
        {
            currentWaveIndex = -1;
            waveProgressEventChannel.RaiseEvent(GetWaveProgressString());
        }
        
        public void StartNextWave()
        {
            if (IsLastWave)
            {
                waveEndEventChannel.RaiseEvent();
                return;
            }

            currentWaveIndex++;
            ResetWaveState();
            
            waveProgressEventChannel.RaiseEvent(GetWaveProgressString());
            currentWaveCoroutine = StartCoroutine(SpawnWave(waveData.Waves[currentWaveIndex]));
        }

        private IEnumerator SpawnWave(WaveDataSO.WaveData wave)
        {
            isSpawning = true;
            for (; currentSpawnInfoIndex < wave.SpawnInfos.Count; currentSpawnInfoIndex++)
            {
                var spawnInfo = wave.SpawnInfos[currentSpawnInfoIndex];
                while (currentEnemyIndex < spawnInfo.Count)
                {
                    currentEnemyIndex++;
                    enemySpawnChannel.RaiseEvent(spawnInfo.EnemyToSpawn);

                    yield return new WaitForSeconds(spawnInfo.DelayBetweenSpawns);
                }
                currentEnemyIndex = 0; 
                yield return new WaitForSeconds(spawnInfo.DelayAfterGroup);
            }
            isSpawning = false;
        }
        private void CurrentWaveSpawnedEnemyDestroyed()
        {
            if(isSpawning)  return;
            
            waveEndEventChannel.RaiseEvent();

            if (!IsLastWave)
                StartCoroutine(DelayCoroutine());
        }

        private IEnumerator DelayCoroutine()
        {
            yield return new WaitForSeconds(waveData.DelayBetweenWaves);
            StartNextWave();
        }

        private void PauseWave()
        {
            if (currentWaveCoroutine == null) return;
    
            StopCoroutine(currentWaveCoroutine);
            StopAllCoroutines();
            currentWaveCoroutine = null;
            isSpawning = false;
        }

        private void ResumeWave()
        {
            if (currentWaveIndex < 0 || currentWaveIndex >= waveData.Waves.Count) return;
            
            currentWaveCoroutine = StartCoroutine(SpawnWave(waveData.Waves[currentWaveIndex]));
            isSpawning = true;
        }
        
        private void ResetWaveState()
        {
            currentSpawnInfoIndex = 0;
            currentEnemyIndex = 0;
        }

        private string GetWaveProgressString()
        {
            return $"{currentWaveIndex + 1}/{waveData.TotalWaves}";
        }
    }
}