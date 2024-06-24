using Modules.TowerModule.Scripts;
using UnityEngine;

namespace Modules.TowerModule.Test.Scripts
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