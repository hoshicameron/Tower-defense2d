using System;
using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Sensors
{
    [CreateAssetMenu(fileName = "Circular Sensor", menuName = "Gameplay/Funcs/Circular Sensor")]
    public class CircularSensor : SensorBase
    {
        private CircleCollider2D circleCollider2D;
        public override void Initialize(Collider2D colliderShape, LayerMask layerMask, Action<GameObject> collide)
        {
            targetLayerMask = layerMask;
            onCollide = collide;
            circleCollider2D = colliderShape as CircleCollider2D;
        }

        public override void Detect()
        {
            var hits = Physics2D.OverlapCircle(circleCollider2D.transform.position, circleCollider2D.radius, targetLayerMask);

            if (hits != null)
            {
                onCollide?.Invoke(hits.gameObject);
            }
        }
    }
}