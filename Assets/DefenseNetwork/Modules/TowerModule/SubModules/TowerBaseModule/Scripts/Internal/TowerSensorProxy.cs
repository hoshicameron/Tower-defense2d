#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class TowerSensorProxy : MonoBehaviour
    {
        private CircleCollider2D collider;
        private LayerMask targetLayerMask;
        private Action<Transform?> onTargetDetected;
        private HashSet<Transform> detectedTargets = new();
        private Transform currentTarget;
        public Transform CurrentTarget
        {
            get => currentTarget;
            private set
            {
                if (currentTarget != value)
                {
                    currentTarget = value;
                    onTargetDetected?.Invoke(currentTarget);
                }
            }
        }

        private void Awake()
        {
            collider = GetComponent<CircleCollider2D>();
            collider.isTrigger = true;
        }

        public void Initialize(LayerMask layermask, Action<Transform?> targetDetected  )
        {
            onTargetDetected = targetDetected;
            targetLayerMask = layermask;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (((1 << other.gameObject.layer) & targetLayerMask) == 0) return;
            detectedTargets.Add(other.transform);
            UpdateClosestTarget();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (detectedTargets.Remove(other.transform))
            {
                UpdateClosestTarget();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            UpdateClosestTarget();
        }

        private void UpdateClosestTarget()
        {
            Transform closestEnemy = null;
            var closestDistance = Mathf.Infinity;
            var detectionPoint = transform.GetWorldPositionFromHighestParent();

            foreach (var detectedTarget in detectedTargets)
            {
                var distance = Vector2.Distance(detectionPoint, detectedTarget.position);
                if (!(distance < closestDistance)) continue;
                closestEnemy = detectedTarget;
                closestDistance = distance;
            }

            CurrentTarget = closestEnemy;
        }
    }
}