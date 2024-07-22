using System;
using DefenseNetwork.Core.EventChannels.DataObjects.Enums;
using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using DefenseNetwork.CoreTowerDefense.Enums;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;


namespace DefenseNetwork.Modules.EnvironmentModule.Scripts
{
    public class TowerSpot : MonoBehaviour, ISelectable
    {
        [Header("Channel")] 
        [SerializeField] private TowerDeployRequestEventChannelSO towerDeployRequestEventChannel;
        
        [Header("Events")]
        [SerializeField] public UnityEvent onTowerSpotSelected;
        
        public void Select()
        {
            onTowerSpotSelected?.Invoke();
        }

        public void DeployMissileTower(int cost)
        {
            var deployRequest = new TowerDeployRequest
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
            var deployRequest = new TowerDeployRequest
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
