using System;
using Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modules.TowerModule.SubModules.TowerBaseModule.Scripts
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
