using UnityEngine;

namespace Modules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality
{
    [CreateAssetMenu(fileName = "Circular Sensor", menuName = "Gameplay/Funcs/Circular Sensor")]
    public class CircularSensor : SensorBase
    {
        public override void Detect()
        {
            var hits = Physics2D.OverlapCircleAll(detectionPoint.position, detectionRadius, targetLayerMask);

            if (hits.Length > 0)
            {
                oncollide?.Invoke(hits[0].gameObject);
            }
        }
    }
}