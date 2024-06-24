using System;
using UnityEngine;

namespace Modules.TowerModule.Scripts
{
    public class TowerService : MonoBehaviour
    {
        [SerializeField] private Internal.Tower towerPrefab;

        public void CreatTower(Action<Transform> targetDetected,Action targetOutOfRange )
        {
            var newTower = Instantiate(towerPrefab);
        }
        
    }
}
