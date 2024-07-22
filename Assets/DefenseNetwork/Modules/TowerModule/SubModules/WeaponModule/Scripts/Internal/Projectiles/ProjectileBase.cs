using System;
using System.Collections;
using DefenseNetwork.CoreTowerDefense.DataTransferObjects;
using DefenseNetwork.CoreTowerDefense.Enums;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Sensors;
using DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Data;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.Projectiles
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] protected HitEventChannelSO hitEventChannel;
        [SerializeField] protected GameStateEventChannelSO gameStateEventChannel;
        
        [Space]
        [Header("Components")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] protected Collider2D collider;

        [SerializeField] protected SensorBase sensorTemplate;
        
        [field:Space]
        [field:Header("Data")]
        [field:SerializeField] public ProjectileDataSO ProjectileDataSo { get; private set; }

        public Action<GameObject> onCollide;
        
        protected SensorBase sensor;
        protected Transform target;
        
        protected bool canMove = true;

        private void OnEnable()
        {
            gameStateEventChannel.OnEventRaised += HandleGameStateChange;
        }

        private void OnDisable()
        {
            gameStateEventChannel.OnEventRaised -= HandleGameStateChange;
        }

        private void HandleGameStateChange(GameState state)
        {
            switch (state)
            {
                case GameState.Playing:
                    canMove = true;
                    break;
                case GameState.Paused:
                case GameState.Won:
                case GameState.Lost:
                    canMove = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(ProjectileDataSo.LifeTime);
            DestroyBullet();
        }
        protected void Collide(GameObject detectedObject)
        {
            onCollide?.Invoke(detectedObject);
            
            hitEventChannel.RaiseEvent(new HitDTO{HittedObject = detectedObject,Damage = ProjectileDataSo.Damage});
            DestroyBullet();
        }

        private void DestroyBullet()
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }

        public abstract void Initialize(Transform projectileTarget, Transform projectileSpawnTransform);
        protected abstract void Update();

        private void OnValidate()
        {
            UpdateProjectileVisuals(ProjectileDataSo);
        }

        private void UpdateProjectileVisuals(ProjectileDataSO data)
        {
            if (spriteRenderer != null && data != null)
                spriteRenderer.sprite = data.Sprite;
        }
    }
}