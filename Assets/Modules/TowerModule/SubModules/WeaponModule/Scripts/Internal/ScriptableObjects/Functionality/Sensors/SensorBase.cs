using System;
using UnityEngine;

namespace Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality.Sensors
{
    public abstract class SensorBase : ScriptableObject
    {
        protected LayerMask targetLayerMask;
        public Action<GameObject> onCollide;

        public abstract void Initialize(Collider2D colliderShape, LayerMask layerMask, Action<GameObject> collide);
        
        public abstract void Detect();
        
        
    }
}