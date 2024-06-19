using UnityEditor.VersionControl;
using UnityEngine;

namespace Tower.Scripts
{
    
    public abstract class RotatorBase : ScriptableObject
    {
        protected Transform head;
        protected float turnSpeed;
        
        public void Initialize(Transform head, float turnSpeed)
        {
            this.head = head;
            this.turnSpeed = turnSpeed;
        }

        public abstract void Rotate(Transform target,float deltaTime);

        
    }
}