using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.ScriptableObjects.StateMachine.States
{
    [CreateAssetMenu(fileName = "Idle State", menuName = "Gameplay/Tower State/Idle")]
    public class IdleState : TowerState
    {
        public override void Enter(TowerBase towerBase)
        {
            Debug.Log("Idle State");
        }

        public override void Update(TowerBase towerBase)
        {
            
        }

        public override void Exit(TowerBase towerBase)
        {
            
        }
    }
}