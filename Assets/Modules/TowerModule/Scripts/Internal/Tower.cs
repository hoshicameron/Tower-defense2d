using System;
using Modules.TowerModule.Scripts.Internal.ScriptableObjects.Datas;
using Modules.TowerModule.Scripts.Internal.ScriptableObjects.Sensors;
using Modules.TowerModule.Scripts.Internal.ScriptableObjects.StateMachine;
using Modules.TowerModule.Scripts.Internal.ScriptableObjects.StateMachine.States;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.TowerModule.Scripts.Internal
{
    [RequireComponent(typeof(Weapon))]
    public class Tower : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TowerDataSO towerDataSo;
        [SerializeField] private SpriteRenderer bodyRenderer;
        
        [Header("Weapon Attach Point")]
        [SerializeField] private Transform head;
        
        [SerializeField] private SensorBase sensorType;
        
        [Space]
        [Header("Events")]
        [SerializeField] public UnityEvent<Transform> onTargetDetected;
        [SerializeField] public UnityEvent onTargetOutOfRange;

        private SensorBase sensor;
        
        private TowerState idleState;
        private TowerState shootState;
        private TowerState currentState;

        

        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            idleState = ScriptableObject.CreateInstance<IdleState>();
            shootState = ScriptableObject.CreateInstance<ShootState>();

            sensor = (SensorBase)ScriptableObject.CreateInstance(sensorType.GetType());
            
            
            SetupRenderers();
            sensor.Initialize(new SensorBase.SensorConfig
            {
                detectionPoint = transform,
                detectionRadius = towerDataSo.Range,
                targetLayerMask = towerDataSo.DetectionLayer,
                onTargetDetected = OnTargetDetected,
                onTargetOutOfRange = OnTargetOutOfRange
            });
            
            ChangeToIdleState();
        }

        private void OnTargetOutOfRange()
        {
            ChangeToIdleState();
            onTargetOutOfRange?.Invoke();
        }

        private void OnTargetDetected(Transform target)
        {
            ChangeToShootState();
            onTargetDetected?.Invoke(target);
        }

        public void SetupRenderers()
        {
            if (bodyRenderer is not null)
                bodyRenderer.sprite = towerDataSo.BaseSprite;
        }

        private void Update()
        {
            sensor.Detect();
            currentState.Update(this);
        }

        private void ChangeState(TowerState newState)
        {
            if (currentState == newState) return;
            
            currentState?.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }

        public void ChangeToIdleState() => ChangeState(idleState);
        public void ChangeToShootState() => ChangeState(shootState);
        
        private void OnDrawGizmosSelected()
        {
            if (towerDataSo == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,towerDataSo.Range);
        }
    }
}