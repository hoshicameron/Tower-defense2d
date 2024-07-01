using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.Events;

namespace DefenseNetwork.Modules.EnvironmentModule.Scripts
{
    public class TowerSpot : MonoBehaviour
    {
        [Header("Channel")] 
        [SerializeField] private IntEventChannelSO modifyPointsEventChannel;
        [SerializeField] private Vector2EventChannelSO missileTowerDeployPositionEventChannel;
        [SerializeField] private Vector2EventChannelSO bulletDeployPositionEventChannel;
        [Header("Events")]
        [SerializeField] public UnityEvent onTowerSpotSelected;
        
        private void OnMouseUp()
        {
            onTowerSpotSelected?.Invoke();
        }

        public void DeployMissileTower(int cost)
        {
            modifyPointsEventChannel.RaiseEvent(-cost);
            missileTowerDeployPositionEventChannel.RaiseEvent(transform.position);
            Destroy(gameObject);
        }
        
        public void DeployBulletTower(int cost)
        {
            modifyPointsEventChannel.RaiseEvent(-cost);
            bulletDeployPositionEventChannel.RaiseEvent(transform.position);
            Destroy(gameObject);
        }
        
        
    }
}
