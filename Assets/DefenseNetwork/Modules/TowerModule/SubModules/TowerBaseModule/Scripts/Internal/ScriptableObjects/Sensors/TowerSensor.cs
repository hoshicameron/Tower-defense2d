#nullable enable
using System;
using UnityEngine;
using Utilities;

namespace DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.ScriptableObjects.Sensors
{
    [CreateAssetMenu(fileName = "Tower Sensor", menuName = "Gameplay/Funcs/Tower Sensor")]
    public class TowerSensor : ScriptableObject
    {
        private SensorConfig sensorConfig;
        
        public class SensorConfig
        {
            public CircleCollider2D CircleCollider { get; set; }
            public LayerMask TargetLayerMask { get; set; }
            public Action<Transform?> onTargetDetected;
            public Vector2 DetectionPoint => CircleCollider.transform.GetWorldPositionFromHighestParent();
            public float DetectionRadius => CircleCollider.radius;
            
            
        }
        
        private Transform target;
        public Transform Target
        {
            get => target;
            protected set
            {
                target = value;
                sensorConfig.onTargetDetected.Invoke(target);
            }
        }

        public void Initialize(SensorConfig newSensorConfig )
        { 
            sensorConfig = newSensorConfig;
        }

        public void Detect()
        {
            if(HasTargetWithinRange())
                return;
            
            var hits = Physics2D.OverlapCircleAll(sensorConfig.DetectionPoint, sensorConfig.DetectionRadius, sensorConfig.TargetLayerMask);

            if (hits.Length > 0)
            {
                Transform closestEnemy = null;
                var closestDistance = Mathf.Infinity;
                foreach (var hit in hits)
                {
                    var distance = Vector2.Distance(sensorConfig.DetectionPoint, hit.transform.position);
                    if (!(distance < closestDistance)) continue;
                    closestEnemy = hit.transform;
                    closestDistance = distance;
                }

                if (closestEnemy != null)
                {
                    Target = closestEnemy;
                }
            }
            else
            {
                Target = null;
            }
        }
        
        private bool HasTargetWithinRange()
        {
            if (Target == null) return false;
            return Vector2.Distance(Target.position, sensorConfig.DetectionPoint) <= sensorConfig.DetectionRadius;
        }

    }
}