using DefenseNetwork.Core.EventChannels.DataObjects.Enums;
using UnityEngine;

namespace DefenseNetwork.Modules.UIModule.GamePlay.TowerSpotView.Scripts
{
    public struct TowerData : ITowerData
    {
        public string Name { get; }
        public int DeployCost { get; }
        public int UpgradeCost { get; }
        public GameObject Prefab { get; }
        public TowerType Type { get; }
        public Sprite Sprite { get; }
        public string Description { get; }
        public int SellIncome { get; }
    }
}