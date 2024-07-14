using System;
using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Sensors
{
    [CreateAssetMenu(fileName = "Box Sensor", menuName = "Gameplay/Behaviours/Sensors/Box Sensor")]
    public class BoxSensor : SensorBase
    {
        private BoxCollider2D boxCollider2D;
        public override void Initialize(Collider2D colliderShape, LayerMask layerMask, Action<GameObject> collide)
        {
            targetLayerMask = layerMask;
            onDetect = collide;
            boxCollider2D = (BoxCollider2D)colliderShape;
        }

        public override void Detect()
        {
            
            var hits = 
                Physics2D.OverlapBoxAll(boxCollider2D.transform.position, boxCollider2D.size, 0f, targetLayerMask);
            
            if(hits is { Length:>0 } )
                onDetect?.Invoke(hits[0].gameObject);
        }
    }
}