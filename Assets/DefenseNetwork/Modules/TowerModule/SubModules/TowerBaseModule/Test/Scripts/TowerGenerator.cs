using DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts;

using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Test.Scripts
{
    public class TowerGenerator : MonoBehaviour
    {
        [SerializeField] private TowerService towerService;
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
                towerService.CreatTower(
                    target => Debug.Log($"Target: {target} Spotted!"),
                    () => Debug.Log("Target Not in Range!"));
        }

    }
}