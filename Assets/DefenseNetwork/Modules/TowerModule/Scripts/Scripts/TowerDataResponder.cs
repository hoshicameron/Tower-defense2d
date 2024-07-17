using System;
using System.Collections.Generic;
using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.Core.EventChannels.DataObjects.Enums;
using DefenseNetwork.Modules.TowerModule.Scripts.Scripts.ScriptableObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.Scripts.Scripts
{
    public class TowerDataResponder : MonoBehaviour
    {
        /*[Header("Event Channel")] 
        [SerializeField] private TowerDataRequestEventChannelSO towerDataRequestEventChannel;

        [Space] [Header("Data")] 
        [SerializeField] private AvailableTowersDataSO availableTowersDataSo;

        private void OnEnable()
        {
            towerDataRequestEventChannel.OnEventRaised += HandleDataRequest;
        }

        private void HandleDataRequest(TowerDataRequestDTO request)
        {
            foreach (var towerData in availableTowersDataSo.GetTowersType())
            {
                
            }
            request.InvokeResult();
        }*/
    }
}