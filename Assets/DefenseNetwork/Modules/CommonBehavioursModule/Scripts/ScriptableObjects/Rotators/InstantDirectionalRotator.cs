using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Rotators
{
    [CreateAssetMenu(fileName = "Instant Directional Rotator" , menuName = "Gameplay/Behaviours/Rotators/Instant directional Rotator")]
    class InstantDirectionalRotator : DirectionalRotator
    {
        public override void Rotate(Vector2 direction, float deltaTime, float rotationOffset = 90f)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rotationOffset;
            transformToRotate.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}