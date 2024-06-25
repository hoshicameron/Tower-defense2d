using System;
using System.Collections;
using Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Data;
using Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality.Movers;
using Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality.Rotators;
using Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality.Sensors;
using UnityEngine;

namespace Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ProjectileDataSO projectileDataSo;
        [SerializeField] private Mover movingType;
        [SerializeField] private SensorBase sensorType;
        [SerializeField] private RotatorBase rotatorType;
        [SerializeField] private Collider2D collider;

        public event Action<GameObject> onCollide;
        private Mover mover;
        private SensorBase sensor;
        private RotatorBase rotator;
        private Transform target;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(projectileDataSo.LifeTime);
            DestroyBullet();
        }

        public void Initialize(Transform target)
        {
            this.target = target;
            mover = (Mover)ScriptableObject.CreateInstance(movingType.GetType());
            mover.Initialize(target, transform, projectileDataSo.Speed);
            
            sensor = (SensorBase)ScriptableObject.CreateInstance(sensorType.GetType());
            sensor.Initialize(collider,projectileDataSo.DetectionLayer,Collide);

            if (rotatorType != null)
            {
                rotator = (RotatorBase)ScriptableObject.CreateInstance(rotatorType.GetType());
                rotator.Initialize(transform,projectileDataSo.RotationSpeed);
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
            mover.Move(Time.deltaTime);
            
            if (target == null) return;
            if (rotator != null) rotator.Rotate(target, Time.deltaTime);
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