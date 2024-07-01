using DefenseNetwork.Modules.TowerModule.Scripts.Scripts.ScriptableObjects;
using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.Events;

namespace DefenseNetwork.Modules.TowerModule.Scripts.Scripts
{
    public class Tower : MonoBehaviour
    {
        [Header("Channel")] 
        [SerializeField] private IntEventChannelSO modifyPointEventChannel;
        [SerializeField] private IntEventChannelSO updatePointEventChannel;
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
            updatePointEventChannel.OnEventRaised += PointUpdated; 
        }

        private void OnDisable()
        {
            updatePointEventChannel.OnEventRaised -= PointUpdated; 
        }

        private void PointUpdated(int point)
        {
            onPointUpdated?.Invoke(towerDataSo.Upgrades[towerLevel].UpgradeCost<=point);
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
            Destroy(towerInternal);
            modifyPointEventChannel.RaiseEvent(towerDataSo.Upgrades[towerLevel].UpgradeCost);
            InstantiateTowerInternals(towerDataSo.Upgrades[++towerLevel].Prefab);
        }

        public void SellTower()
        {
            modifyPointEventChannel.RaiseEvent(towerDataSo.Upgrades[towerLevel].SellIncome);
            Destroy(gameObject);
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
