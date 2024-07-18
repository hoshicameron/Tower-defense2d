using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.Scripts.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New AvailableTowersDataSO", menuName = "Gameplay/Data/AvailableTowersData")]
    public class AvailableTowersDataSO : ScriptableObject
    {
        [field:SerializeField] public List<TowerDataSO> Towers { get; private set; }

        public List<ITowerData> GetTowersType()
        {
            return Towers.Select(towerDataSo => towerDataSo.BaseTowerData).ToList();
        }
    }
}