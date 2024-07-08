using System;
using System.Collections.Generic;
using DefenseNetwork.Core.EventChannels.DataObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.EnemiesModule.Scripts.Tests
{
    public class EnemyTest : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private HitEventChannelSO hitEventChannelSo;
        [SerializeField] private List<Vector3> path;
        [SerializeField] private Enemy enemyPrefab;

        private Enemy enemy;
        private void Start()
        {
            enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.Initialize(path);
        }

        private void Update()
        {
            
            if(Input.GetKeyDown(KeyCode.Space))
                hitEventChannelSo.RaiseEvent(new HitDTO { hittedObject = enemy.gameObject, damage = 2 });
        }
    }
}
