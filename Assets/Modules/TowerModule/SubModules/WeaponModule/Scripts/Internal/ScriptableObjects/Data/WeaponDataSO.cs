using Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.Projectiles;
using UnityEngine;

namespace Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Data
{
    [CreateAssetMenu(fileName = "New WeaponData", menuName = "Gameplay/Weapons/Weapon Data")]
    public class WeaponDataSO : ScriptableObject
    {
        [field:SerializeField] public float TurnSpeed { get; private set; } = 10.0f;
        [field:SerializeField] public float FireRate { get; private set; } = 5;
        [field:SerializeField] public Projectile ProjectilePrefab { get; private set; }
        [field:SerializeField] public Sprite Sprite { get; private set; }
    }
}