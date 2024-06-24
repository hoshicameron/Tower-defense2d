using UnityEngine;

namespace Modules.TowerModule.Scripts.Internal.ScriptableObjects.StateMachine
{
    public abstract class TowerState : ScriptableObject
    {
        public abstract void Enter(Modules.TowerModule.Scripts.Internal.Tower tower);
        public abstract void Update(Modules.TowerModule.Scripts.Internal.Tower tower);
        public abstract void Exit(Modules.TowerModule.Scripts.Internal.Tower tower);
    }
}