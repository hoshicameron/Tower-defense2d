using System;
using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Sensors
{
    public abstract class SensorBase : ScriptableObject
    {
        protected LayerMask targetLayerMask;
        public Action<GameObject> onDetect;

        public abstract void Initialize(Collider2D colliderShape, LayerMask layerMask, Action<GameObject> detect);
        public abstract void Detect();
        
        
    }
}