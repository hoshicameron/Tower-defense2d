using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Movers
{
    [CreateAssetMenu(fileName = "Direct Mover", menuName = "Gameplay/Behaviours/Movers/Direct Mover")]
    public class DirectTargetMover : TargetMover
    {
        private Vector2 direction;
        public override void Initialize(Transform objectToMove, float speed,Transform target )
        {
            base.Initialize(objectToMove,speed, target);
            if (target != null) 
                direction = (target.position - objectToMove.position).normalized;
            
        }

        public override void Move(float deltaTime)
        {
            if (transformToMove == null) return;
            
            transformToMove.Translate(direction * (speed * deltaTime));
        }
    }
}