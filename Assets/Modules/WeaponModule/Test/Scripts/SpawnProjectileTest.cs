using System.Collections.Generic;
using Modules.WeaponModule.Scripts.Internal;
using Modules.WeaponModule.Scripts.Internal.ScriptableObjects;
using Modules.WeaponModule.Scripts.Internal.ScriptableObjects.Data;
using Unity.VisualScripting;
using UnityEngine;

namespace Modules.WeaponModule.Test.Scripts
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