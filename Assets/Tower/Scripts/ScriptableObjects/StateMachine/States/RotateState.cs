using UnityEngine;

namespace Tower.Scripts.StateMachine.States
{
    [CreateAssetMenu(fileName = "Rotate State", menuName = "Gameplay/Tower State/Rotate")]
    public class RotateState : TowerState
    {
        public override void Enter(Tower tower)
        {
            
        }

        public override void Update(Tower tower)
        {
            tower.RotateTowardsTarget();
            if(tower.IsFacingTarget())
                tower.ChangeToShootState();
        }

        public override void Exit(Tower tower)
        {
            
        }
    }
}