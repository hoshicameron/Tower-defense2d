using System;
using UnityEngine;

namespace Modules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality
{
    public abstract class SensorBase : ScriptableObject
    {
        protected Transform detectionPoint;
        protected float detectionRadius;
        protected LayerMask targetLayerMask;
        public Action<GameObject> oncollide;

        public void Initialize(Transform pointToCheck, float range, LayerMask layerMask, Action<GameObject> collide)
        {
            detectionPoint = pointToCheck;
            detectionRadius = range;
            targetLayerMask = layerMask;
            oncollide = collide;
        }

        public abstract void Detect();
    }
}