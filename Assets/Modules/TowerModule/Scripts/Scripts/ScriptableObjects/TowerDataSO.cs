using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modules.TowerModule.Scripts.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New TowerData", menuName = "Gameplay/TowerData")]
    public class TowerDataSO : ScriptableObject
    {
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField] public List<TowerData> Upgrades { get; private set; }

        public TowerData BaseTowerData => Upgrades[0];


        [System.Serializable]
        public class TowerData
        {
            public int DeployCost;
            public int UpgradeCost;
            public GameObject Prefab;
            public int SellIncome => DeployCost - (int)(DeployCost * 0.2f);
        }
    }
}