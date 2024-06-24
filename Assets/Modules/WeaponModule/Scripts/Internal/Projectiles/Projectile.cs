using System;
using System.Collections;
using Modules.WeaponModule.Scripts.Internal.ScriptableObjects.Data;
using Modules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality;
using Modules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality.Movers;
using UnityEngine;

namespace Modules.WeaponModule.Scripts.Internal.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ProjectileDataSO projectileDataSo;
        [SerializeField] private Mover movingType;
        [SerializeField] private SensorBase sensorType;

        public event Action<GameObject> onCollide;
        private Mover mover;
        private SensorBase sensor;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(projectileDataSo.LifeTime);
            DestroyBullet();
        }

        private void Update()
        {
            mover.Move(Time.deltaTime);
        }

        public void Initialize(Transform target)
        {
            mover = (Mover)ScriptableObject.CreateInstance(movingType.GetType());
            mover.Initialize(target, transform, projectileDataSo.Speed);
            sensor = (SensorBase)ScriptableObject.CreateInstance(sensorType.GetType());
            sensor.Initialize(transform, projectileDataSo.Range,projectileDataSo.DetectionLayer,Collide);
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

        private void OnValidate()
        {
            UpdateProjectileVisuals(projectileDataSo);
        }

        private void UpdateProjectileVisuals(ProjectileDataSO data)
        {
            if (spriteRenderer != null && data != null)
                spriteRenderer.sprite = data.Sprite;
        }

        private void OnDrawGizmosSelected()
        {
            if (projectileDataSo == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,projectileDataSo.Range);
        }
    }
}