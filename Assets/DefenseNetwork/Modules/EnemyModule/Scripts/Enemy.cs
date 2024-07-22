using System;
using System.Collections.Generic;
using DefenseNetwork.CoreTowerDefense.DataTransferObjects;
using DefenseNetwork.CoreTowerDefense.Enums;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects;
using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Movers;
using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Rotators;
using DefenseNetwork.Modules.EnemyModule.Scripts.ScriptableObjects.Data;
using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.Events;

namespace DefenseNetwork.Modules.EnemyModule.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private VoidEventChannelSO reachedPlayerBaseEventChannel;
        [SerializeField] private HitEventChannelSO hitEventChannel;
        [SerializeField] private GameObjectEventChannelSO enemyDestroyedEventChannel;
        [SerializeField] private IntEventChannelSO enemyDestroyedRewardEventChannel;
        [SerializeField] private GameStateEventChannelSO gameStateEventChannel;
        
        [Space] [Header("Data")] 
        [SerializeField] private EnemyDataSO enemyDataSo;
        
        [Space][Header("Components")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform bodyTransform;

        [Space] [Header("Behaviours")] 
        [SerializeField] private DirectionalMover directionalMoverTemplate;
        [SerializeField] private DirectionalRotator directionalRotatorTemplate;
        [SerializeField] private HealthBehaviour healthTemplate;
        [SerializeField] private float rotationOffset;
        

        [Space]
        [Header("Events")]
        public UnityEvent<int, int> onHealthChanged;

        private HealthBehaviour health;
        private List<Vector3> movementPath;
        private DirectionalMover directionalMover;
        private DirectionalRotator directionalRotator;
        private int currentWaypointIndex;

        private bool canMove = true;
        private void OnEnable()
        {
            health = Instantiate(healthTemplate);
            health.OnHealthChanged += HealthChanged;
            health.OnDeath += Death;
            health.Initialize(enemyDataSo.Health);
            
            hitEventChannel.OnEventRaised += Hit;
            
            gameStateEventChannel.OnEventRaised += HandleGameStateChange;
        }
        private void OnDisable()
        {
            hitEventChannel.OnEventRaised -= Hit;
            health.OnDeath -= Death;
            health.OnHealthChanged -= HealthChanged;
            
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
        
        private void HealthChanged(int currentHealth, int maxHealth) => onHealthChanged?.Invoke(currentHealth,maxHealth);
        private void Death()
        {
            enemyDestroyedEventChannel.RaiseEvent(gameObject);
            enemyDestroyedRewardEventChannel.RaiseEvent(enemyDataSo.RewardGoldAmount);
            Destroy(gameObject);
        }

        private void Hit(HitDTO hitData)
        {
            if(hitData.HittedObject == gameObject)
                health.TakeDamage(hitData.Damage);
        }


        public void Initialize(List<Vector3> path)
        {
            movementPath = path;
            transform.position = movementPath[0];
            currentWaypointIndex++;

            if (directionalMoverTemplate != null)
            {
                directionalMover = (DirectionalMover)ScriptableObject.CreateInstance(directionalMoverTemplate.GetType());
                directionalMover.Initialize(transform, enemyDataSo.Speed);
            }

            if (directionalRotatorTemplate != null)
            {
                directionalRotator = (DirectionalRotator)ScriptableObject.CreateInstance(directionalRotatorTemplate.GetType());
                directionalRotator.Initialize(bodyTransform, enemyDataSo.RotationSpeed);
            }
        }

        private void Update()
        {
            if(canMove == false)    return;
            if(movementPath == null || movementPath.Count==0)   return;
            
            if (currentWaypointIndex < movementPath.Count)
            {
                var targetPosition = movementPath[currentWaypointIndex];
                var direction = (targetPosition - transform.position).normalized;
                directionalMover.SetDirection(direction);
                directionalMover.Move(Time.deltaTime);
                directionalRotator.Rotate(direction,Time.deltaTime, rotationOffset);
                
                if (IsReachedTarget(targetPosition))
                    currentWaypointIndex++;
            }
            else
            {
                reachedPlayerBaseEventChannel.RaiseEvent();
                enemyDestroyedEventChannel.RaiseEvent(gameObject);
                Destroy(gameObject);
            }
        }

        private bool IsReachedTarget(Vector3 targetPosition)
        {
            return Vector3.Distance(transform.position, targetPosition) <= 0.1f;
        }
        
        private void OnValidate()
        {
            WeaponDataValueChanged(enemyDataSo);
        }

        private void WeaponDataValueChanged(EnemyDataSO data) => UpdateWeaponVisuals(data);

        private void UpdateWeaponVisuals(EnemyDataSO data)
        {
            if (spriteRenderer == null || data == null) return;
            
            spriteRenderer.sprite = data.Sprite;
            gameObject.layer = data.EnemyLayer;
        }
    }
}