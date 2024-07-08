using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Rotators
{
    
    public abstract class TargetRotator : ScriptableObject
    {
        private const float angleTolerance = 10f;
        protected Transform rotationPoint;
        protected float turnSpeed;
        
        public void Initialize(Transform rotationPoint, float turnSpeed)
        {
            this.rotationPoint = rotationPoint;
            this.turnSpeed = turnSpeed;
        }

        public abstract void Rotate(Transform target,float deltaTime, float rotationOffset = 90f);

        public bool IsFacingTarget(Transform target)
        {
            Vector2 direction = target.position - rotationPoint.position;
            direction.Normalize();

            return Vector2.Angle(rotationPoint.forward, direction) <= angleTolerance;
        }
    }
}