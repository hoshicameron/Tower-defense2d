using UnityEngine;

namespace Tower.Scripts.StateMachine.States
{
    [CreateAssetMenu(fileName = "Shoot State", menuName = "Gameplay/Tower State/Shoot")]
    public class ShootState : TowerState
    {
        public override void Enter(Tower tower)
        {
            tower.StartShooting();
        }

        public override void Update(Tower tower)
        {
            if(tower.Sensor.CanShoot() == false)
                tower.ChangeToIdleState();
            
            tower.RotateTowardsTarget();
        }

        public override void Exit(Tower tower)
        {
            
        }
    }
}