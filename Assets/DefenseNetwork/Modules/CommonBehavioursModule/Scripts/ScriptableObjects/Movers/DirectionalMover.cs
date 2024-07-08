using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Movers
{
    public abstract class DirectionalMover : ScriptableObject
    {
        protected Vector3 direction;
        protected Transform transformToMove;
        protected float speed;

        public virtual void Initialize(Transform transformToMove, float speed)
        {
            this.transformToMove = transformToMove;
            this.speed = speed;
        }

        public void SetDirection(Vector3 movementDirection) => direction = movementDirection;

        public abstract void Move(float deltaTime);
    }
}