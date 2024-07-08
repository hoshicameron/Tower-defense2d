using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefenseNetwork.Modules.EnemiesModule.Scripts.Tests
{
    public class EnemyTest : MonoBehaviour
    {
        [SerializeField] private List<Vector3> path;
        [SerializeField] private Enemy enemyPrefab;

        private void Start()
        {
            var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.Initialize(path);
        }
    }
}
