using UnityEngine;

namespace Tower.Scripts.StateMachine.States
{
    [CreateAssetMenu(fileName = "Idle State", menuName = "Gameplay/Tower State/Idle")]
    public class IdleState : TowerState
    {
        public override void Enter(Tower tower)
        {
            tower.StopShooting();
        }

        public override void Update(Tower tower)
        {
            if (tower.Sensor.Target)
            {
                tower.ChangeToRotateState();
            }
        }

        public override void Exit(Tower tower)
        {
            
        }
    }
}