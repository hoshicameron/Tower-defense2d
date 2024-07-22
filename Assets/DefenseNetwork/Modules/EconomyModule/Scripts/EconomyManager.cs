using System;
using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using DefenseNetwork.CoreTowerDefense.Enums;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.EconomyModule.Scripts
{
    public class EconomyManager : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private TowerDeployRequestEventChannelSO towerDeployRequestEventChannel;
        [SerializeField] private TowerDeployRequestEventChannelSO towerDeploySuccessChannel;
        [SerializeField] private TowerModificationRequestEventChannelSO towerModificationRequestChannel;
        [SerializeField] private IntEventChannelSO goldUpdateEventChannel;
        [SerializeField] private IntEventChannelSO enemyDestroyedRewardEventChannel;

        [SerializeField] private int initialGold = 200;

        private int currentGold;

        public int CurrentGold
        {
            get => currentGold;
            private set
            {
                currentGold = value;
                goldUpdateEventChannel.RaiseEvent(currentGold);
            }
        }

        private void Awake()
        {
            CurrentGold = initialGold;
        }

        private void OnEnable()
        {
            towerDeployRequestEventChannel.OnEventRaised += HandleTowerDeployRequest;
            towerModificationRequestChannel.OnEventRaised += HandleTowerModificationRequest;
            enemyDestroyedRewardEventChannel.OnEventRaised += HandleEnemyDestroyReward;
        }

        private void OnDisable()
        {
            towerDeployRequestEventChannel.OnEventRaised -= HandleTowerDeployRequest;
            towerModificationRequestChannel.OnEventRaised -= HandleTowerModificationRequest;
            enemyDestroyedRewardEventChannel.OnEventRaised -= HandleEnemyDestroyReward;
        }
        
        
        private void HandleEnemyDestroyReward(int rewardAmount)
        {
            AddGold(rewardAmount);
        }
        
        private void HandleTowerModificationRequest(TowerModificationRequest request)
        {
            switch (request.Operation)
            {
                case TowerOperation.Upgrade:
                    if (CanAfford(request.Cost))
                    {
                        DeductGold(request.Cost);
                        request.InvokeResult(RequestResult.Succeed, $" Tower upgraded successfully");
                    }
                    else
                    {
                        request.InvokeResult(RequestResult.Failure, "Insufficient funds");
                    }
                    break;
                case TowerOperation.Sell:
                    AddGold(request.Cost);
                    request.InvokeResult(RequestResult.Succeed, $" Tower sold successfully");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleTowerDeployRequest(TowerDeployRequest request)
        {
            if (CanAfford(request.DeployCost))
            {
                DeductGold(request.DeployCost);
                towerDeploySuccessChannel.RaiseEvent(request);
                request.InvokeResult(RequestResult.Succeed, $"{request.TowerType.ToString()} deployed successfully");
            }
            else
            {
                request.InvokeResult(RequestResult.Failure, "Insufficient funds");
            }
        }
        
        public int GetCurrentGold() => CurrentGold;

        public void AddGold(int amount) => CurrentGold += amount;

        public void DeductGold(int amount) => CurrentGold -= amount;

        private bool CanAfford(int cost) => CurrentGold >= cost;
    }
}