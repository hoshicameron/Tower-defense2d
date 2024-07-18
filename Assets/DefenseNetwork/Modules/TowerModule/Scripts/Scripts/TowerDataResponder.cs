using System;
using System.Linq;
using DefenseNetwork.CoreTowerDefense.Requests;
using DefenseNetwork.Modules.TowerModule.Scripts.Scripts.ScriptableObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.Scripts.Scripts
{
    public class TowerDataResponder : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private TowerDataRequestEventChannelSO towerDataRequestEventChannel;

        [Space] [Header("Data")] 
        [SerializeField] private AvailableTowersDataSO availableTowersDataSo;

        private void OnEnable()
        {
            towerDataRequestEventChannel.OnEventRaised += HandleDataRequest;
        }

        private void OnDisable()
        {
            towerDataRequestEventChannel.OnEventRaised -= HandleDataRequest;
        }

        private void HandleDataRequest(TowerDataRequest request)
        {
            var towers = availableTowersDataSo.GetTowersType().ToList();
            request.InvokeResult(towers);
        }
    }
}