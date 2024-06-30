using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Data
{
    [CreateAssetMenu(fileName = "New ProjectileData", menuName = "Gameplay/Data/Projectile Data")]
    public class ProjectileDataSO : ScriptableObject
    {
        [field:SerializeField] public float LifeTime { get; private set; }
        [field:SerializeField] public float Speed { get; private set; }
        [field:SerializeField] public int Damage { get; private set; }
        [field:SerializeField]public float RotationSpeed { get; private set; }
        [field:SerializeField]public LayerMask DetectionLayer { get; private set; }
        [field:SerializeField] public Sprite Sprite { get; set; }
        
    }
}