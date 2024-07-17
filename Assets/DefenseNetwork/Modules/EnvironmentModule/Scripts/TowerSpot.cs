using System;
using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.Core.EventChannels.DataObjects.Enums;
using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.Events;

namespace DefenseNetwork.Modules.EnvironmentModule.Scripts
{
    public class TowerSpot : MonoBehaviour
    {
        [Header("Channel")] 
        [SerializeField] private TowerDeployRequestEventChannelSO towerDeployRequestEventChannel;
        
        [Header("Events")]
        [SerializeField] public UnityEvent onTowerSpotSelected;
        
        private void OnMouseUp()
        {
            onTowerSpotSelected?.Invoke();
        }

        public void DeployMissileTower(int cost)
        {
            var deployRequest = new TowerDeployRequestDTO
            {
                TowerType = TowerType.MissileTower,
                DeployCost = cost,
                DeployPosition = transform.position
            };
            deployRequest.OnRequestResult += HandleDeployResult;
            towerDeployRequestEventChannel.RaiseEvent(deployRequest);
            
        }
        
        public void DeployBulletTower(int cost)
        {
            var deployRequest = new TowerDeployRequestDTO
            {
                TowerType = TowerType.BulletTower,
                DeployCost = cost,
                DeployPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y),

            };
            
            deployRequest.OnRequestResult += HandleDeployResult;
            towerDeployRequestEventChannel.RaiseEvent(deployRequest);
        }

        private void HandleDeployResult(RequestResult result, string message)
        {
            switch(result)
            {
                case RequestResult.Succeed:
                    Debug.Log($"Success: {message}");
                    Destroy(gameObject);
                    break;
                case RequestResult.Failure:
                    Debug.Log($"Failure: {message}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }
    }
}
