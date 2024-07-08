using System.Collections.Generic;
using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Movers;
using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Rotators;
using DefenseNetwork.Modules.EnemiesModule.Scripts.ScriptableObjects.Data;
using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefenseNetwork.Modules.EnemiesModule.Scripts
{
    
    public class Enemy : MonoBehaviour
    {
        [FormerlySerializedAs("enemyReachedTarget")]
        [Header("Event Channel")] 
        [SerializeField] private VoidEventChannelSO enemyReachedPlayerBaseEvent;
        
        [Space] [Header("Data")] 
        [SerializeField] private EnemyDataSO enemyDataSo;
        
        [Space][Header("Components")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform bodyTransform;

        [Space] [Header("Behaviours")] 
        [SerializeField] private DirectionalMover directionalMoverType;
        [SerializeField] private DirectionalRotator directionalRotatorType;
        [SerializeField] private float rotationOffset;

        private List<Vector3> movementPath;
        private DirectionalMover directionalMover;
        private DirectionalRotator directionalRotator;
        private int currentWaypointIndex;
        public void Initialize(List<Vector3> path)
        {
            movementPath = path;
            transform.position = movementPath[0];
            currentWaypointIndex++;

            if (directionalMoverType != null)
            {
                directionalMover = (DirectionalMover)ScriptableObject.CreateInstance(directionalMoverType.GetType());
                directionalMover.Initialize(transform, enemyDataSo.Speed);
            }

            if (directionalRotatorType != null)
            {
                directionalRotator = (DirectionalRotator)ScriptableObject.CreateInstance(directionalRotatorType.GetType());
                directionalRotator.Initialize(bodyTransform, enemyDataSo.RotationSpeed);
            }
        }

        private void Update()
        {
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
                enemyReachedPlayerBaseEvent.RaiseEvent();
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