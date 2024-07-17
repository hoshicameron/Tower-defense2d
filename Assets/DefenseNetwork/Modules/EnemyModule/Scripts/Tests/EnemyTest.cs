using System.Collections.Generic;
using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.DataTransferObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.EnemyModule.Scripts.Tests
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
                hitEventChannelSo.RaiseEvent(new HitDTO { HittedObject = enemy.gameObject, Damage = 2 });
        }
    }
}
