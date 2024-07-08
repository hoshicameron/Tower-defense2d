using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Movers
{
    [CreateAssetMenu(fileName = "FollowTarget Mover", menuName = "Gameplay/Behaviours/Movers/FollowTarget Mover")]
    public class FollowTargetTargetMover : TargetMover
    {
        public override void Move(float deltaTime)
        {
            if (transformToMove == null || target == null)  return;
            
            transformToMove.position = Vector2.MoveTowards(transformToMove.position, target.position, speed * deltaTime);
        }
    }
}