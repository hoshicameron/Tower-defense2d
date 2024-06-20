using System;
using UnityEngine;

namespace Tower.Scripts
{
    [CreateAssetMenu(fileName = "New Sensor", menuName = "Gameplay/Funcs/Sensor")]
    public class Sensor : ScriptableObject
    {
        private Transform detectionPoint; 
        private float detectionRadius;
        private LayerMask targetLayerMask;
        private event Action<Transform> onEnemyDetected;

        private Transform target;
        public Transform Target
        {
            get => target;
            private set
            {
                target = value;
                onEnemyDetected?.Invoke(target);
            }
        }
        
        private void OnEnable()
        {
            target = null;
        }

        public void Initialize(Transform point,float range,LayerMask layerMask, Action<Transform> enemyDetected)
        {
            detectionPoint = point;
            detectionRadius = range;
            targetLayerMask = layerMask;
            onEnemyDetected = enemyDetected;
        }

        public void Detect()
        {
            if(HasTargetWithinRange())
                return;
            
            var hits = Physics2D.OverlapCircleAll(detectionPoint.position, detectionRadius, targetLayerMask);

            if (hits.Length > 0)
            {
                Transform closestEnemy = null;
                var closestDistance = Mathf.Infinity;
                foreach (var hit in hits)
                {
                    var distance = Vector2.Distance(detectionPoint.position, hit.transform.position);
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
            }
        }
        
        private bool HasTargetWithinRange()
        {
            if (Target == null) return false;
            return Vector2.Distance(Target.position, detectionPoint.position) <= detectionRadius;
        }

        public bool CanShoot() => HasTargetWithinRange();

        

    }
}