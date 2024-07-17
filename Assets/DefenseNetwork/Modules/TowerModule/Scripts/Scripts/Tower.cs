using System;
using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.Core.EventChannels.DataObjects.Enums;
using DefenseNetwork.Modules.TowerModule.Scripts.Scripts.ScriptableObjects;
using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefenseNetwork.Modules.TowerModule.Scripts.Scripts
{
    public class Tower : MonoBehaviour
    {
        [Header("Channel")] 
        [SerializeField] private TowerModificationRequestEventChannelSO towerModifyChannel;
        [SerializeField] private IntEventChannelSO goldUpdateEventChannel;
        [SerializeField] private Vector2EventChannelSO towerSpotDeployPositionEventChannel;
        [Space]
        [Header("Data")]
        [SerializeField] private TowerDataSO towerDataSo;
        [Space]
        [Header("Events")]
        [SerializeField] public UnityEvent<int> onTowerSelected;
        [SerializeField] public UnityEvent onTowerSelectedWhileMax;
        [SerializeField] public UnityEvent<bool> onPointUpdated;

        private int towerLevel;
        private GameObject towerInternal;

        private void OnEnable()
        {
            goldUpdateEventChannel.OnEventRaised += PointUpdated;
        }

        private void OnDisable()
        {
            goldUpdateEventChannel.OnEventRaised -= PointUpdated; 
        }

        private void PointUpdated(int point)
        {
            onPointUpdated?.Invoke(towerDataSo.Upgrades[towerLevel].UpgradeCost <= point);
        }

        private void Start()
        {
            SetupTower();
        }

        public void SetupTower()
        {
            InstantiateTowerInternals(towerDataSo.Upgrades[towerLevel].Prefab);
        }

        private void InstantiateTowerInternals(GameObject prefabToInstantiate)
        {
            towerInternal = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
            towerInternal.transform.SetParent(transform);
        }

        public void UpgradeTower()
        {
            var upgradeRequest = new TowerModificationRequestDTO
            {
                Cost = towerDataSo.Upgrades[towerLevel].UpgradeCost,
                Operation = TowerOperation.Upgrade
            };
            
            upgradeRequest.OnRequestResult += HandleTowerUpgradeResult;
            towerModifyChannel.RaiseEvent(upgradeRequest);
        }

        private void HandleTowerUpgradeResult(RequestResult result, string message)
        {
            switch (result)
            {
                case RequestResult.Succeed:
                    Debug.Log(message);
                    Destroy(towerInternal);
                    InstantiateTowerInternals(towerDataSo.Upgrades[++towerLevel].Prefab);
                    break;
                case RequestResult.Failure:
                    Debug.Log(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }

        public void SellTower()
        {
            var sellRequest = new TowerModificationRequestDTO
            {
                Cost = towerDataSo.Upgrades[towerLevel].SellIncome,
                Operation = TowerOperation.Sell
            };
            
            sellRequest.OnRequestResult += HandleTowerSellResult;
            
            towerModifyChannel.RaiseEvent(sellRequest);
            
        }

        private void HandleTowerSellResult(RequestResult result, string message)
        {
            switch (result)
            {
                case RequestResult.Succeed:
                    Debug.Log(message);
                    towerSpotDeployPositionEventChannel.RaiseEvent(transform.position);
                    Destroy(gameObject);
                    break;
                case RequestResult.Failure:
                    Debug.Log(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }

        private void OnMouseUp()
        {
            TowerSelected();
        }

        private void TowerSelected()
        {
            if (!IsTowerAtMaxLevel())
                onTowerSelected?.Invoke(towerDataSo.Upgrades[towerLevel].UpgradeCost);
            else
                onTowerSelectedWhileMax?.Invoke();
        }

        private bool IsTowerAtMaxLevel() => towerLevel >= towerDataSo.Upgrades.Count -1;
    }
}
