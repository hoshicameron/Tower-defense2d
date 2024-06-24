using UnityEngine;

namespace Modules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality.Movers
{
    public abstract class Mover : ScriptableObject
    {
        protected Transform target;
        protected Transform objectToMove;
        protected float speed;

        public virtual void Initialize(Transform target, Transform objectToMove, float speed)
        {
            this.target = target;
            this.objectToMove = objectToMove;
            this.speed = speed;
        }
        public abstract void Move(float deltaTime);
    }
}