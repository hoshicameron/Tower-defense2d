using System.Collections.Generic;
using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
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

        private void SpawnEnemy(EnemySpawnRequest spawnRequest)
        {
            
            var newEnemy = Instantiate(gameObjectEnemyDict[spawnRequest.EnemyToSpawn], transform.position, Quaternion.identity);
            newEnemy.Initialize(pathRequester.CurrentPath);
            spawnRequest.onEnemySpawned?.Invoke(newEnemy.gameObject);
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