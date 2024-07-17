using System;
using System.Collections.Generic;
using DefenseNetwork.Modules.TowerModule.Scripts.Scripts.Enums;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.Scripts.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New TowerData", menuName = "Gameplay/Data/TowerData")]
    public class TowerDataSO : ScriptableObject
    {
        [field:SerializeField] public List<TowerData> Upgrades { get; private set; }

        public TowerData BaseTowerData => Upgrades[0];

        [Serializable]
        public struct TowerData
        {
            public string Name { get; private set; }
            public int DeployCost { get; private set; }
            public int UpgradeCost { get; private set; }
            public GameObject Prefab{ get; private set; }
            public TowerType Type { get; private set; }
            public Sprite Sprite{ get; private set; }
            public string Description { get; private set; }
            
            public int SellIncome => DeployCost - (int)(DeployCost * 0.2f);
        }
    }
}