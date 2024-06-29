using GameSystemsCookbook;
using Modules.TowerModule.Scripts.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    [Header("Channel")] 
    [SerializeField] private IntEventChannelSO towerSoldEventChannel;
    [Space]
    [Header("Data")]
    [SerializeField] private TowerDataSO towerDataSo;
    [Space]
    [Header("Events")]
    [SerializeField] public UnityEvent<int> onTowerSelected;
    [SerializeField] public UnityEvent onTowerSelectedWhileMax;

    private int towerLevel;
    private GameObject towerInternal;

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
        InstantiateTowerInternals(towerDataSo.Upgrades[++towerLevel].Prefab);
    }

    public void SellTower()
    {
        towerSoldEventChannel.RaiseEvent(towerDataSo.Upgrades[towerLevel].SellIncome);
        Destroy(gameObject);
    }
    
    private void OnMouseDown()
    {
        TowerSelected();
    }

    private void TowerSelected()
    {
        if (!IsTowerAtLastLevel())
            onTowerSelected?.Invoke(towerDataSo.Upgrades[towerLevel].UpgradeCost);
        else
            onTowerSelectedWhileMax?.Invoke();
    }

    private bool IsTowerAtLastLevel() => towerLevel >= towerDataSo.Upgrades.Count -1;
}
