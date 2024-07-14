using System;
using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Sensors
{
    [CreateAssetMenu(fileName = "Circular Sensor", menuName = "Gameplay/Behaviours/Sensors/Circular Sensor")]
    public class CircularSensor : SensorBase
    {
        private CircleCollider2D circleCollider2D;
        public override void Initialize(Collider2D colliderShape, LayerMask layerMask, Action<GameObject> collide)
        {
            targetLayerMask = layerMask;
            onDetect = collide;
            circleCollider2D = colliderShape as CircleCollider2D;
        }

        public override void Detect()
        {
            var hits = Physics2D.OverlapCircleAll(circleCollider2D.transform.position, circleCollider2D.radius, targetLayerMask);

            if (hits is { Length: > 0 })
            {
                onDetect?.Invoke(hits[0].gameObject);
            }
        }
    }
}