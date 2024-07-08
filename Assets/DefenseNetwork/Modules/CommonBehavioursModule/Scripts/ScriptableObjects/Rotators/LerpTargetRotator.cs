using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Rotators
{
    [CreateAssetMenu(fileName = "New Lerp Rotator" , menuName = "Gameplay/Behaviours/Rotators/Lerp Rotator")]
    public  class LerpTargetRotator : TargetRotator
    {
        public override void Rotate(Transform target, float deltaTime, float rotationOffset = 90f)
        {
            var direction = (target.position - rotationPoint.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rotationOffset;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            rotationPoint.rotation = Quaternion.Slerp(rotationPoint.rotation, rotation, turnSpeed * deltaTime);
        }
    }
}