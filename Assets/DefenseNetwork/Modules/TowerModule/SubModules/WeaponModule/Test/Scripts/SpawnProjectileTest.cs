using DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal;
using Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal;
using UnityEngine;

namespace Modules.TowerModule.SubModules.WeaponModule.Test.Scripts
{
    public class SpawnProjectileTest : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Weapon weapon;

        private void Start()
        {
            weapon.SetTarget(target);
        }
    }
}