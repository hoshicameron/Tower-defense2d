using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.EnvironmentModule
{
    public class TowerSpotDeployer : MonoBehaviour
    {
        [Header("Channel")] 
        [SerializeField] private Vector2EventChannelSO towerSpotDeployPositionEventChannel;
        [Space]
        [SerializeField] private GameObject towerSpotPrefab;

        private void OnEnable()
        {
            towerSpotDeployPositionEventChannel.OnEventRaised += DeployTowerSpot;
        }
        private void OnDisable()
        {
            towerSpotDeployPositionEventChannel.OnEventRaised += DeployTowerSpot;
        }
        
        private void DeployTowerSpot(Vector2 positionToDeploy)
        {
            Instantiate(towerSpotPrefab, positionToDeploy, Quaternion.identity);
        }
    }
}