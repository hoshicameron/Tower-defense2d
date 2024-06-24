using UnityEngine;

namespace Modules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality.Movers
{
    [CreateAssetMenu(fileName = "Direct Mover", menuName = "Gameplay/Funcs/Direct Mover")]
    public class DirectMover : Mover
    {
        private Vector2 direction;
        public override void Initialize(Transform target, Transform objectToMove, float speed)
        {
            base.Initialize(target,objectToMove,speed);
            direction = (target.position - objectToMove.position).normalized;
        }

        public override void Move(float deltaTime)
        {
            objectToMove.Translate(direction * (speed * deltaTime));
        }
    }
}