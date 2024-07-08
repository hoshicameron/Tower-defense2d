using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Movers
{
    public abstract class TargetMover : ScriptableObject
    {
        protected Transform target;
        protected Transform transformToMove;
        protected float speed;
        public virtual void Initialize(Transform transformToMove, float speed,Transform target)
        {
            this.transformToMove = transformToMove;
            this.speed = speed;
            this.target = target;
        }
        public abstract void Move(float deltaTime);
    }
}