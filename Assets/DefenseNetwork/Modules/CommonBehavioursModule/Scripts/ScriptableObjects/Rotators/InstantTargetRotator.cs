using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Rotators
{
    [CreateAssetMenu(fileName = "Instant Target Rotator" , menuName = "Gameplay/Behaviours/Rotators/Instant Target Rotator")]
    public class InstantTargetRotator : TargetRotator
    {
        public override void Rotate(Transform target, float deltaTime, float rotationOffset = 90f)
        {
            if(target== null)   return;
            
            var direction = target.position - rotationPoint.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rotationOffset;
            rotationPoint.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}