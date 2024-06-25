using System.Collections.Generic;
using Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Data;
using Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality;
using Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality.Rotators;
using UnityEngine;

namespace Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private RotatorBase rotatorType;
        [SerializeField] private WeaponDataSO weaponDataSo;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private List<Transform> projectileSpawnPositions;

        private Transform target;
        private RotatorBase rotator;
        private Spawner projectileSpawner;
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
            rotator = (RotatorBase)ScriptableObject.CreateInstance(rotatorType.GetType());
            rotator.Initialize(transform, weaponDataSo.TurnSpeed);

            projectileSpawner = ScriptableObject.CreateInstance<Spawner>();
            projectileSpawner.Initialize(projectileSpawnPositions , weaponDataSo.ProjectilePrefab);
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

        private void RotateTowardsTarget() => rotator.Rotate(target, Time.deltaTime);
        private bool IsFacingTarget() => target != null && rotator.IsFacingTarget(target);
        public void SetTarget(Transform newTarget) => target = newTarget;
        public void RemoveTarget() => target = null;
        private void StartShooting()
        {
            if(IsShooting)  return;
            shootRoutine ??= StartCoroutine(projectileSpawner.ShootRoutine(target, weaponDataSo.DelayBetweenShoots));
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