using DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal;
using DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.ScriptableObjects.StateMachine;
using UnityEngine;

namespace Modules.TowerModule.Scripts.Internal.ScriptableObjects.StateMachine.States
{
    [CreateAssetMenu(fileName = "Shoot State", menuName = "Gameplay/Tower State/Shoot")]
    public class ShootState : TowerState
    {
        public override void Enter(TowerBase towerBase)
        {
            Debug.Log("Shoot State");
        }

        public override void Update(TowerBase towerBase)
        {
            
        }

        public override void Exit(TowerBase towerBase)
        {
            
        }
    }
}