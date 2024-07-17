using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.Scripts.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New AvailableTowersDataSO", menuName = "Gameplay/Data/AvailableTowersData")]
    public class AvailableTowersDataSO : ScriptableObject
    {
        public List<TowerDataSO> Towers { get; private set; }

        public List<TowerDataSO.TowerData> GetTowersType()
        {
            return Towers.Select(towerDataSo => towerDataSo.BaseTowerData).ToList();
        }
    }
}