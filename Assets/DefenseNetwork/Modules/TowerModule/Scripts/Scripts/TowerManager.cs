using System;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.Scripts.Scripts
{
    public class TowerManager : MonoBehaviour
    {
        [Header("Channel")] 
        [SerializeField] private Vector2EventChannelSO missileTowerDeployPositionEventChannel;
        [SerializeField] private Vector2EventChannelSO bulletDeployPositionEventChannel;
        [Space] 
        [SerializeField] private GameObject bulletTowerPrefab;
        [SerializeField] private GameObject missileTowerPrefab;

        private void OnEnable()
        {
            missileTowerDeployPositionEventChannel.OnEventRaised += DeployMissileTower; 
            bulletDeployPositionEventChannel.OnEventRaised += DeployBulletTower;
        }

        private void OnDisable()
        {
            missileTowerDeployPositionEventChannel.OnEventRaised -= DeployMissileTower; 
            bulletDeployPositionEventChannel.OnEventRaised -= DeployBulletTower;
        }

        private void DeployBulletTower(Vector2 positionToDeploy) => Instantiate(bulletTowerPrefab, positionToDeploy, Quaternion.identity);
        private void DeployMissileTower(Vector2 positionToDeploy) => Instantiate(missileTowerPrefab, positionToDeploy, Quaternion.identity);
    }
}
