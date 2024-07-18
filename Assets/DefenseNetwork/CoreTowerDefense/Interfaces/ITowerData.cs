using DefenseNetwork.Core.EventChannels.DataObjects.Enums;
using UnityEngine;

public interface ITowerData
{
    string Name { get; }
    int DeployCost { get; }
    int UpgradeCost { get; }
    GameObject Prefab { get; }
    TowerType Type { get; }
    Sprite Sprite { get; }
    string Description { get; }
    int SellIncome { get; }
}