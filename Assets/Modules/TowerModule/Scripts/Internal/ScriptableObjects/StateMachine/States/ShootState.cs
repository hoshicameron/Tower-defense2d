using UnityEngine;

namespace Modules.TowerModule.Scripts.Internal.ScriptableObjects.StateMachine.States
{
    [CreateAssetMenu(fileName = "Shoot State", menuName = "Gameplay/Tower State/Shoot")]
    public class ShootState : TowerState
    {
        public override void Enter(Tower tower)
        {
            Debug.Log("Shoot State");
        }

        public override void Update(Tower tower)
        {
            
        }

        public override void Exit(Tower tower)
        {
            
        }
    }
}