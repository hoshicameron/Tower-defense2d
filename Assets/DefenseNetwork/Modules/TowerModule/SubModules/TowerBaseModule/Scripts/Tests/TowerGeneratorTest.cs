using System;
using DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Tests
{
    public class TowerGeneratorTest : MonoBehaviour
    {
        [SerializeField] private TowerBase towerBasePrefab;
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
                CreatTower(
                    target => Debug.Log($"Target: {target} Spotted!"),
                    () => Debug.Log("Target Not in Range!"));
        }
        private void CreatTower(Action<Transform> targetDetected,Action targetOutOfRange )
        {
            var newTower = Instantiate(towerBasePrefab);
        }

    }
}