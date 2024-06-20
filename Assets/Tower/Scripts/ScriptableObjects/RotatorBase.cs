using UnityEditor.VersionControl;
using UnityEngine;

namespace Tower.Scripts
{
    
    public abstract class RotatorBase : ScriptableObject
    {
        [SerializeField] private float angleTolerance = 10f;
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