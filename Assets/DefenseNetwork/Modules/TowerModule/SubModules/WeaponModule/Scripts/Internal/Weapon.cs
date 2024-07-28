﻿using System;
using System.Collections.Generic;
using DefenseNetwork.CoreTowerDefense.Enums;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Rotators;
using DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Data;
using DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality;
using GameSystemsCookbook;
using JetBrains.Annotations;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Weapon : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private GameStateEventChannelSO gameStateEventChannel;
        [SerializeField] private AudioEventChannelSO audioEffectEventChannel; 
        
        [Space][Header("Behaviours")]
        [SerializeField] private TargetRotator targetRotatorTemplate;

        [SerializeField] private ProjectileSpawner projectileSpawnerTemplate;
        
        [Space]
        [Header("Components")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        [Space]
        [Header("Projectiles Spawn Position")]
        [SerializeField] private List<Transform> projectileSpawnPositions;
        
        [Space]
        [Header("Data")]
        [SerializeField] private WeaponDataSO weaponDataSo;

        private Transform target;
        private TargetRotator targetRotator;
        private ProjectileSpawner projectileSpawner;
        private Coroutine shootRoutine;

        private bool isWeaponOperational = true;

        public bool IsShooting { get; private set; }

        private void Awake()
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
        }

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
                    isWeaponOperational = true;
                    break;
                case GameState.Paused:
                case GameState.Won:
                case GameState.Lost:
                    isWeaponOperational = false;
                    StopShooting();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            targetRotator = Instantiate(targetRotatorTemplate);
            targetRotator.Initialize(transform, weaponDataSo.TurnSpeed);

            projectileSpawner = Instantiate(projectileSpawnerTemplate);
            
            projectileSpawner.Initialize(
                projectileSpawnPositions ,
                weaponDataSo.BulletPrefab,
                weaponDataSo.DelayBetweenShoots,
                () => audioEffectEventChannel.RaiseEvent(weaponDataSo.ShootAudioClip)
                );
        }

        private void Update()
        {
            if(isWeaponOperational == false)    return;
            if(target == null)  return;
                
            RotateTowardsTarget();
            
            if(IsFacingTarget())
                StartShooting();
        }

        private void RotateTowardsTarget() => targetRotator.Rotate(target, Time.deltaTime);
        private bool IsFacingTarget() => targetRotator.IsFacingTarget(target);
        public void SetTarget([CanBeNull] Transform newTarget)
        {
            if (newTarget == null)  StopShooting();
            
            target = newTarget;
            projectileSpawner.SetTarget(target);
        }

        private void RemoveTarget()
        {
            target = null;
        }

        private void StartShooting()
        {
            if(IsShooting)  return;
            shootRoutine ??= StartCoroutine(projectileSpawner.ShootRoutine());
            IsShooting = true;
        }

        private void StopShooting()
        {
            if (shootRoutine != null)
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