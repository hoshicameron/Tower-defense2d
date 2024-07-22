﻿using System;
using System.Collections;
using System.Collections.Generic;
using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
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
        [SerializeField] private EnemySpawnRequestChannelSO enemySpawnRequestChannel;
        [SerializeField] private GameObjectEventChannelSO enemyDestroyedEventChannel;
        [SerializeField] private StringEventChannelSO waveProgressEventChannel;
        [SerializeField] private GameStateEventChannelSO gameStateEventChannel;
        [SerializeField] private VoidEventChannelSO startGameChannel;
        
        [Space]
        [Header("Data")]
        [SerializeField] private WaveDataSO waveData;
        

        private bool IsLastWave => currentWaveIndex >= waveData.Waves.Count - 1;
        
        private int currentSpawnInfoIndex;
        private int currentEnemyIndex;
        private int currentWaveIndex;
        
        private List<GameObject> activeEnemies = new();
        private Coroutine currentWaveCoroutine;
        
        private bool isSpawning;
        
        private void OnEnable()
        {
            Initialize();
            enemyDestroyedEventChannel.OnEventRaised += HandleEnemyDestroyed;
            gameStateEventChannel.OnEventRaised += HandleGameStateChange;
            startGameChannel.OnEventRaised += StartSpawnEnemies;
        }
        private void OnDisable()
        {
            enemyDestroyedEventChannel.OnEventRaised -= HandleEnemyDestroyed;
            gameStateEventChannel.OnEventRaised -= HandleGameStateChange;
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
                    enemySpawnRequestChannel.RaiseEvent(new EnemySpawnRequest 
                    { 
                        EnemyToSpawn = spawnInfo.EnemyToSpawn,
                        onEnemySpawned = AddEnemyToActiveList
                    });

                    yield return new WaitForSeconds(spawnInfo.DelayBetweenSpawns);
                }
                currentEnemyIndex = 0; 
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

            if (activeEnemies.Count != 0 || isSpawning) return;
            
            waveEndEventChannel.RaiseEvent();
            
            if (!IsLastWave)
                StartNextWave();
        }

        private void PauseWave()
        {
            if (currentWaveCoroutine == null) return;
    
            StopCoroutine(currentWaveCoroutine);
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