using System;
using System.Collections.Generic;
using DefenseNetwork.Core.EventChannels.DataObjects.Enums;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.Scripts.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New TowerData", menuName = "Gameplay/Data/TowerData")]
    public class TowerDataSO : ScriptableObject
    {
        [field:SerializeField] public List<TowerData> Upgrades { get; private set; }

        public ITowerData BaseTowerData => Upgrades[0];

        [Serializable]
        public struct TowerData : ITowerData
        {
            [field:SerializeField] public string Name { get; private set; }
            [field:SerializeField] public int DeployCost { get; private set; }
            [field:SerializeField] public int UpgradeCost { get; private set; }
            [field:SerializeField] public GameObject Prefab{ get; private set; }
            [field:SerializeField] public TowerType Type { get; private set; }
            [field:SerializeField] public Sprite Sprite{ get; private set; }
            [field:SerializeField] public string Description { get; private set; }
            
            public int SellIncome => DeployCost - (int)(DeployCost * 0.2f);
        }
    }
}