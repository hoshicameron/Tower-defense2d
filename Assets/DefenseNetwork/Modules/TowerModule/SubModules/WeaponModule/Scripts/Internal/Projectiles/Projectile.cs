using System;
using System.Collections;
using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Movers;
using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Rotators;
using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Sensors;
using DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ProjectileDataSO projectileDataSo;
        [SerializeField] private TargetMover movingType;
        [SerializeField] private SensorBase sensorType;
        [FormerlySerializedAs("rotatorType")] [SerializeField] private TargetRotator targetRotatorType;
        [SerializeField] private Collider2D collider;

        public event Action<GameObject> onCollide;
        private TargetMover targetMover;
        private SensorBase sensor;
        private TargetRotator targetRotator;
        private Transform target;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(projectileDataSo.LifeTime);
            DestroyBullet();
        }

        public void Initialize(Transform target)
        {
            this.target = target;
            targetMover = (TargetMover)ScriptableObject.CreateInstance(movingType.GetType());
            targetMover.Initialize(transform, projectileDataSo.Speed, target);
            
            sensor = (SensorBase)ScriptableObject.CreateInstance(sensorType.GetType());
            sensor.Initialize(collider,projectileDataSo.DetectionLayer,Collide);

            if (targetRotatorType != null)
            {
                targetRotator = (TargetRotator)ScriptableObject.CreateInstance(targetRotatorType.GetType());
                targetRotator.Initialize(transform,projectileDataSo.RotationSpeed);
            }
        }
        private void Collide(GameObject collider)
        {
            onCollide?.Invoke(collider);
            DestroyBullet();
        }

        private void DestroyBullet()
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
        
        private void Update()
        {
            targetMover.Move(Time.deltaTime);
            
            if (target == null) return;
            if (targetRotator != null) targetRotator.Rotate(target, Time.deltaTime);
        }

        private void OnValidate()
        {
            UpdateProjectileVisuals(projectileDataSo);
        }

        private void UpdateProjectileVisuals(ProjectileDataSO data)
        {
            if (spriteRenderer != null && data != null)
                spriteRenderer.sprite = data.Sprite;
        }

    }
}