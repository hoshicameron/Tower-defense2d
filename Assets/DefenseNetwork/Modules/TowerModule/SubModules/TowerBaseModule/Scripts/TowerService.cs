using System;
using DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts
{
    public class TowerService : MonoBehaviour
    {
        [SerializeField] private TowerBase towerBasePrefab;

        public void CreatTower(Action<Transform> targetDetected,Action targetOutOfRange )
        {
            var newTower = Instantiate(towerBasePrefab);
        }
        
    }
}
