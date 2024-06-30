using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality.Movers
{
    public abstract class Mover : ScriptableObject
    {
        protected Transform target;
        protected Transform transformToMove;
        protected float speed;

        public virtual void Initialize(Transform target, Transform transformToMove, float speed)
        {
            this.target = target;
            this.transformToMove = transformToMove;
            this.speed = speed;
        }
        public abstract void Move(float deltaTime);
    }
}