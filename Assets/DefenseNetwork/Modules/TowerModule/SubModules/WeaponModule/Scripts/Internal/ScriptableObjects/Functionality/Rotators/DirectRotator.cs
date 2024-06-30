using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality.Rotators
{
    [CreateAssetMenu(fileName = "New Instant Rotator" , menuName = "Gameplay/Funcs/Instant Rotator")]
    public class DirectRotator : RotatorBase
    {
        public override void Rotate(Transform target, float deltaTime)
        {
            var direction = target.position - rotationPoint.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rotationPoint.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}