using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.ScriptableObjects.Sensors
{
    [CreateAssetMenu(fileName = "Tower Sensor", menuName = "Gameplay/Funcs/Tower Sensor")]
    public class TowerSensor : SensorBase
    {
        private void OnEnable()
        {
            target = null;
        }

        public override void Detect()
        {
            if(HasTargetWithinRange())
                return;
            
            var hits = Physics2D.OverlapCircleAll(sensorConfig.detectionPoint.position, sensorConfig.detectionRadius, sensorConfig.targetLayerMask);

            if (hits.Length > 0)
            {
                Transform closestEnemy = null;
                var closestDistance = Mathf.Infinity;
                foreach (var hit in hits)
                {
                    var distance = Vector2.Distance(sensorConfig.detectionPoint.position, hit.transform.position);
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
                target = null;
                sensorConfig.onTargetOutOfRange?.Invoke();
            }
        }
        
        private bool HasTargetWithinRange()
        {
            if (Target == null) return false;
            return Vector2.Distance(Target.position, sensorConfig.detectionPoint.position) <= sensorConfig.detectionRadius;
        }
    }
}