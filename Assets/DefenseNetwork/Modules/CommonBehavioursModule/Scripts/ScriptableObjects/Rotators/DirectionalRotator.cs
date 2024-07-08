using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Rotators
{
    
    public abstract class DirectionalRotator : ScriptableObject
    {
        private const float angleTolerance = 10f;
        protected Transform transformToRotate;
        protected float turnSpeed;
    
        public virtual void Initialize(Transform transformToRotate, float turnSpeed)
        {
            this.transformToRotate = transformToRotate;
            this.turnSpeed = turnSpeed;
        }

        public abstract void Rotate(Vector2 direction, float deltaTime, float rotationOffset = 90f);
    }
}