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
        [SerializeField] private Vector2EventChannelSO towerPositionEventChannel;

        [Space] 
        [SerializeField] private int points;
        [SerializeField] private GameObject gameObjectToInstantiate;
        
        
        private void Start()
        {
            updatePointsEventChannel.RaiseEvent(points);
            Debug.Log("Notify");
            modifyPointsEventChannel.OnEventRaised += UpdatePoints;
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

    }
}
