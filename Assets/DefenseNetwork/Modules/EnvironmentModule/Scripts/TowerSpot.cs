using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefenseNetwork.Modules.EnvironmentModule.Scripts
{
    public class TowerSpot : MonoBehaviour
    {
        [Header("Channel")] 
        [SerializeField] private IntEventChannelSO modifyPointsEventChannel;
        [SerializeField] private IntEventChannelSO towerTypeEventChannel;
        [SerializeField] private Vector2EventChannelSO towerPositionEventChannel;
        [Header("Events")]
        [SerializeField] public UnityEvent onTowerSpotSelected;
        
        private void OnMouseDown()
        {
            onTowerSpotSelected?.Invoke();
        }

        public void DeployTower(int towerType, int cost)
        {
            modifyPointsEventChannel.RaiseEvent(-cost);
            towerTypeEventChannel.RaiseEvent(towerType);
            towerPositionEventChannel.RaiseEvent(transform.position);
        }
    }
}
