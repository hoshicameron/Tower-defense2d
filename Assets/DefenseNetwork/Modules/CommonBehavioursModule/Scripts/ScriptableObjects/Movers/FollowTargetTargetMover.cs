using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Movers
{
    [CreateAssetMenu(fileName = "FollowTarget Mover", menuName = "Gameplay/Behaviours/Movers/FollowTarget Mover")]
    public class FollowTargetTargetMover : TargetMover
    {
        private const float MaxTurnRate = 360f;
        private Vector2 lastDirection;

        public override void Move(float deltaTime)
        {
            if (transformToMove == null) return;
            
        
            Vector2 currentPosition = transformToMove.position;
            Vector2 directionToMove;

            if (target != null)
            {
                Vector2 targetPosition = target.position;
                directionToMove = (targetPosition - currentPosition).normalized;
                lastDirection = directionToMove; 
            }
            else
            {
                directionToMove = lastDirection;
            }

            
            Vector2 currentDirection = transformToMove.up;

            var angle = Vector2.SignedAngle(currentDirection, directionToMove);
            var turnAmount = Mathf.Min(Mathf.Abs(angle), MaxTurnRate * deltaTime);
            if (angle < 0) turnAmount = -turnAmount;

            transformToMove.Rotate(0, 0, turnAmount);

            transformToMove.Translate(Vector2.up * (speed * deltaTime), Space.Self);
        }
    }
}