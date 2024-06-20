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

        public void Initialize(Transform point,float range,LayerMask layerMask, Action<Transform> enemyDetected)
        {
            detectionPoint = point;
            detectionRadius = range;
            targetLayerMask = layerMask;
            onEnemyDetected = enemyDetected;
        }

        public void Detect()
        {
            if(target is not null && IsTargetWithinRange() )
                return;
            
            var hits = Physics2D.OverlapCircleAll(detectionPoint.position, detectionRadius, targetLayerMask); // Check for overlaps within radius and layer

            if (hits.Length <= 0) return;
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
        
        private bool IsTargetWithinRange()=> Vector2.Distance(Target.position, detectionPoint.position) <= detectionRadius;
    }
}