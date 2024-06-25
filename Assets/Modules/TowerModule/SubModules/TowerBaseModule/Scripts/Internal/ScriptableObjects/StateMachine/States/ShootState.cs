using Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal;
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