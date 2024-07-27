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
        [SerializeField] private GameObjectEventChannelSO enemySpawnChannel;
        
        [Space]
        [Header("Components")]
        [SerializeField] private PathRequester pathRequester;

        [Space] 
        [Header("Available Enemies")] 
        [SerializeField] private List<Enemy> availableEnemies;

        private Dictionary<GameObject, Enemy> gameObjectEnemyDict = new();


        private void OnEnable()
        {
            enemySpawnChannel.OnEventRaised += SpawnEnemy;
        }

        private void OnDisable()
        {
            enemySpawnChannel.OnEventRaised -= SpawnEnemy;
        }

        private void SpawnEnemy(GameObject enemyToSpawn)
        {
            var newEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            newEnemy.GetComponent<Enemy>().Initialize(pathRequester.CurrentPath);
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