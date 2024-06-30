using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.ScriptableObjects.StateMachine
{
    public abstract class TowerState : ScriptableObject
    {
        public abstract void Enter(TowerBase towerBase);
        public abstract void Update(TowerBase towerBase);
        public abstract void Exit(TowerBase towerBase);
    }
}