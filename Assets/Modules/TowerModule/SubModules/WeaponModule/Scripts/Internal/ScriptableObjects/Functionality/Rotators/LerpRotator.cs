using UnityEngine;

namespace Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality.Rotators
{
    [CreateAssetMenu(fileName = "New Lerp Rotator" , menuName = "Gameplay/Funcs/Lerp Rotator")]
    public  class LerpRotator : RotatorBase
    {
        public override void Rotate(Transform target, float deltaTime)
        {
            var direction = (target.position - rotationPoint.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            rotationPoint.rotation = Quaternion.Slerp(rotationPoint.rotation, rotation, turnSpeed * deltaTime);
        }
    }
}