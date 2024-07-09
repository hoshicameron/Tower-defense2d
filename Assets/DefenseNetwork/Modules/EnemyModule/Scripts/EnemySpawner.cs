using System.Collections.Generic;
using DefenseNetwork.Core.EventChannels.DataObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.EnemyModule.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Event Channel")]
        [SerializeField] private EnemySpawnRequestChannelSO enemySpawnRequestChannel;
        
        [Space]
        [Header("Components")]
        [SerializeField] private PathRequester pathRequester;

        [Space] 
        [Header("Available Enemies")] 
        [SerializeField] private List<Enemy> availableEnemies;

        private Dictionary<GameObject, Enemy> gameObjectEnemyDict = new();


        private void OnEnable()
        {
            enemySpawnRequestChannel.OnEventRaised += SpawnEnemy;
        }

        private void OnDisable()
        {
            enemySpawnRequestChannel.OnEventRaised -= SpawnEnemy;
        }

        private void SpawnEnemy(EnemySpawnRequestDTO spawnRequestDto)
        {
            
            var newEnemy = Instantiate(gameObjectEnemyDict[spawnRequestDto.EnemyToSpawn], transform.position, Quaternion.identity);
            newEnemy.Initialize(pathRequester.CurrentPath);
            spawnRequestDto.onEnemySpawned?.Invoke(newEnemy.gameObject);
        }

        private void Start()
        {
            foreach (var enemy in availableEnemies)
            {
                gameObjectEnemyDict.Add(enemy.gameObject , enemy);
            }
        }
    }
}