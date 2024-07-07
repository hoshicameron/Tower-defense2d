using System;
using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Sensors
{
    [CreateAssetMenu(fileName = "Box Sensor", menuName = "Gameplay/Funcs/Box Sensor")]
    public class BoxSensor : SensorBase
    {
        private BoxCollider2D boxCollider2D;
        public override void Initialize(Collider2D colliderShape, LayerMask layerMask, Action<GameObject> collide)
        {
            boxCollider2D = (BoxCollider2D)colliderShape;
        }

        public override void Detect()
        {
            var hit = Physics2D.OverlapBox(boxCollider2D.transform.position, boxCollider2D.size, targetLayerMask);
            if(hit != null)
                onCollide?.Invoke(hit.gameObject);
        }
    }
}