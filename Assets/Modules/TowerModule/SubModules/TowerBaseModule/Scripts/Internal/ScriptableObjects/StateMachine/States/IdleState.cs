using Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal;
using UnityEngine;

namespace Modules.TowerModule.Scripts.Internal.ScriptableObjects.StateMachine.States
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