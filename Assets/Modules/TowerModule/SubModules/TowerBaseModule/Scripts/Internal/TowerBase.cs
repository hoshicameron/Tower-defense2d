using System;
using Modules.TowerModule.Scripts.Internal;
using Modules.TowerModule.Scripts.Internal.ScriptableObjects.StateMachine;
using Modules.TowerModule.Scripts.Internal.ScriptableObjects.StateMachine.States;
using Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.ScriptableObjects.Datas;
using Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.ScriptableObjects.Sensors;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal
{
    
    public class TowerBase : MonoBehaviour
    {
        [FormerlySerializedAs("towerDataSo")]
        [Header("References")]
        [SerializeField] private TowerBaseDataSO towerBaseDataSo;
        [SerializeField] private SpriteRenderer bodyRenderer;
        
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
            
            SetupRenderer(towerBaseDataSo);
            sensor.Initialize(new SensorBase.SensorConfig
            {
                detectionPoint = transform,
                detectionRadius = towerBaseDataSo.Range,
                targetLayerMask = towerBaseDataSo.DetectionLayer,
                onTargetDetected = OnTargetDetected,
                onTargetOutOfRange = OnTargetOutOfRange
            });
            
            ChangeToIdleState();
        }
        private void OnValidate()
        {
            ValidateTowerBaseData(towerBaseDataSo);
        }

        private void ValidateTowerBaseData(TowerBaseDataSO data)
        {
            SetupRenderer(data);
        }

        public void SetupRenderer(TowerBaseDataSO data)
        {
            if (bodyRenderer !=null && data != null)
                bodyRenderer.sprite = data.BaseSprite;
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
        public TowerBaseDataSO GetData() => towerBaseDataSo;
       
        private void OnDrawGizmosSelected()
        {
            if (towerBaseDataSo == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,towerBaseDataSo.Range);
        }
    }
}