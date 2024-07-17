using System;
using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.Core.EventChannels.DataObjects.Enums;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.Scripts.Scripts
{
    public class TowerDeployer : MonoBehaviour
    {
        [Header("Channel")] 
        [SerializeField] private TowerDeployRequestEventChannelSO towerDeployChannel;
        
        [Space] 
        [SerializeField] private GameObject bulletTowerPrefab;
        [SerializeField] private GameObject missileTowerPrefab;

        private void OnEnable()
        {
            towerDeployChannel.OnEventRaised += HandleTowerDeployRequest;
        }
        
        private void OnDisable()
        {
            towerDeployChannel.OnEventRaised -= HandleTowerDeployRequest;
        }

        private void HandleTowerDeployRequest(TowerDeployRequestDTO request)
        {
            switch (request.TowerType)
            {
                case TowerType.BulletTower:
                    DeployBulletTower(request.DeployPosition);
                    break;
                case TowerType.MissileTower:
                    DeployMissileTower(request.DeployPosition);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DeployBulletTower(Vector2 positionToDeploy) => Instantiate(bulletTowerPrefab, positionToDeploy, Quaternion.identity);
        private void DeployMissileTower(Vector2 positionToDeploy) => Instantiate(missileTowerPrefab, positionToDeploy, Quaternion.identity);
    }
}
