using UnityEngine;

namespace Modules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality.Rotators
{
    
    public abstract class RotatorBase : ScriptableObject
    {
        private const float angleTolerance = 10f;
        protected Transform head;
        protected float turnSpeed;
        
        public void Initialize(Transform head, float turnSpeed)
        {
            this.head = head;
            this.turnSpeed = turnSpeed;
        }

        public abstract void Rotate(Transform target,float deltaTime);

        public bool IsFacingTarget(Transform target)
        {
            Vector2 direction = target.position - head.position;
            direction.Normalize();

            return Vector2.Angle(head.forward, direction) <= angleTolerance;
        }

        
    }
}