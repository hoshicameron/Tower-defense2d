using System;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.EnvironmentModule.Test.Scripts
{
    public class TowerSpotTest : MonoBehaviour
    {
        [Header("Channel")] 
        [SerializeField] private IntEventChannelSO modifyPointsEventChannel;
        [SerializeField] private IntEventChannelSO updatePointsEventChannel;
        [SerializeField] private IntEventChannelSO towerTypeEventChannel;
        [SerializeField] private Vector2EventChannelSO towerPositionEventChannel;

        [Space] 
        [SerializeField] private int points;
        [SerializeField] private GameObject gameObjectToInstantiate;
        
        public enum TowerType {BulletTower,MissileTower }

        private TowerType towerType;
        private void Start()
        {
            updatePointsEventChannel.RaiseEvent(points);
            Debug.Log("Notify");
            modifyPointsEventChannel.OnEventRaised += UpdatePoints;
            towerTypeEventChannel.OnEventRaised += TowerTypeSelected;
            towerPositionEventChannel.OnEventRaised += TowerPositionRaised;
        }

        private void UpdatePoints(int value)
        {
             points += value;
             updatePointsEventChannel.RaiseEvent(points);
        }

        private void TowerPositionRaised(Vector2 position)
        {
            Debug.Log($"Tower at point {position} must be deployed");
        }

        private void TowerTypeSelected(int type)
        {
            towerType = (TowerType)type;
            Debug.Log($"Selected Tower to deploy is: {towerType}");
        }
    }
}
