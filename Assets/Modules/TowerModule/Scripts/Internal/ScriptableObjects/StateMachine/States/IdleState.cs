using UnityEngine;

namespace Modules.TowerModule.Scripts.Internal.ScriptableObjects.StateMachine.States
{
    [CreateAssetMenu(fileName = "Idle State", menuName = "Gameplay/Tower State/Idle")]
    public class IdleState : TowerState
    {
        public override void Enter(Tower tower)
        {
            Debug.Log("Idle State");
        }

        public override void Update(Tower tower)
        {
            
        }

        public override void Exit(Tower tower)
        {
            
        }
    }
}