using UnityEngine;

namespace Tower.Scripts.StateMachine
{
    public abstract class TowerState : ScriptableObject
    {
        public abstract void Enter(Tower tower);
        public abstract void Update(Tower tower);
        public abstract void Exit(Tower tower);
    }
}