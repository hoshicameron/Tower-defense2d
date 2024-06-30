using System;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.ScriptableObjects.Sensors
{
    public abstract class SensorBase : ScriptableObject
    {
        protected SensorConfig sensorConfig;
        
        public class SensorConfig
        {
            public Transform detectionPoint;
            public float detectionRadius;
            public LayerMask targetLayerMask;
            public Action<Transform> onTargetDetected;
            public Action onTargetOutOfRange;
        }
        
        protected Transform target;
        public Transform Target
        {
            get => target;
            protected set
            {
                target = value;
                sensorConfig.onTargetDetected?.Invoke(target);
            }
        }

        public void Initialize(SensorConfig newSensorConfig )
        {
            sensorConfig = newSensorConfig;
        }
        public abstract void Detect();
    }
}