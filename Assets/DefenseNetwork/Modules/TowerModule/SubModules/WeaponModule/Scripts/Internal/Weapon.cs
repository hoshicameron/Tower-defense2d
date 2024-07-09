using System.Collections.Generic;
using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Rotators;
using DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Data;
using DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Weapon : MonoBehaviour
    {
        [FormerlySerializedAs("rotatorType")] [SerializeField] private TargetRotator targetRotatorType;
        [SerializeField] private WeaponDataSO weaponDataSo;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private List<Transform> projectileSpawnPositions;

        private Transform target;
        private TargetRotator targetRotator;
        private ProjectileSpawner projectileProjectileSpawner;
        private Coroutine shootRoutine;

        public bool IsShooting { get; private set; }

        private void Awake()
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            targetRotator = (TargetRotator)ScriptableObject.CreateInstance(targetRotatorType.GetType());
            targetRotator.Initialize(transform, weaponDataSo.TurnSpeed);

            projectileProjectileSpawner = ScriptableObject.CreateInstance<ProjectileSpawner>();
            projectileProjectileSpawner.Initialize(projectileSpawnPositions , weaponDataSo.ProjectilePrefab);
        }

        private void Update()
        {
            if (target == null)
            {
                StopShooting();
                return;
            }
            
            RotateTowardsTarget();
            if(IsFacingTarget())
                StartShooting();
        }

        private void RotateTowardsTarget() => targetRotator.Rotate(target, Time.deltaTime);
        private bool IsFacingTarget() => target != null && targetRotator.IsFacingTarget(target);
        public void SetTarget(Transform newTarget) => target = newTarget;
        public void RemoveTarget() => target = null;
        private void StartShooting()
        {
            if(IsShooting)  return;
            shootRoutine ??= StartCoroutine(projectileProjectileSpawner.ShootRoutine(target, weaponDataSo.DelayBetweenShoots));
            IsShooting = true;
        }

        private void StopShooting()
        {
            if(!IsShooting) return;
            if (shootRoutine == null) return;
            
            StopCoroutine(shootRoutine);
            shootRoutine = null;
            IsShooting = false;
        }

        private void OnValidate()
        {
            WeaponDataValueChanged(weaponDataSo);
        }

        private void WeaponDataValueChanged(WeaponDataSO data) => UpdateWeaponVisuals(data);

        private void UpdateWeaponVisuals(WeaponDataSO data)
        {
            if (spriteRenderer != null && data != null)
                spriteRenderer.sprite = data.Sprite;
        }

        public void SetupWeaponRenderer()
        {
            if(weaponDataSo != null)
                UpdateWeaponVisuals(weaponDataSo);
        }
    }
}