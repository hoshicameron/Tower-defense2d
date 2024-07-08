using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Movers
{
    [CreateAssetMenu(fileName = "ConstantDirectionMover", menuName = "Gameplay/Behaviours/Movers/Constant Direction Mover")]
    class ConstantDirectionMover : DirectionalMover
    {
        public override void Move(float deltaTime)
        {
            if (transformToMove == null) return;
        
            transformToMove.Translate(direction * (speed * deltaTime));
        }
    }
}