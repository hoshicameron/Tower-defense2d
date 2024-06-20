using System;
using System.Collections.Generic;
using Tower.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tower.Scripts
{
    public class Tower : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TowerDataSO towerDataSo;
        [SerializeField] private SpriteRenderer headRenderer;
        [SerializeField] private SpriteRenderer bodyRenderer;
        
        [Header("Rotation")]
        [SerializeField] private Transform head;
        [SerializeField] private RotatorBase rotator;

        [Header("Shooting")] 
        [SerializeField] private ShooterBase gun;
        [field: SerializeField] public Sensor Sensor { get; private set; }
        [SerializeField] private List<Transform> projectileSpawnPoint;
        [field:Header("States")]
        [SerializeField] public TowerState idleState;
        [SerializeField] public TowerState shootState;
        [SerializeField] public TowerState rotateState;

        private bool isShooting;
        private Coroutine shootRoutine;
        private TowerState currentState;

        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            SetupRenderers();
            rotator.Initialize(head, towerDataSo.TurnSpeed);
            Sensor.Initialize(transform,towerDataSo.Range,towerDataSo.DetectionLayer, _ => { });
            gun.Initialize(projectileSpawnPoint);
            ChangeToIdleState();
        }

        public void SetupRenderers()
        {
            if (headRenderer is not null)
                headRenderer.sprite = towerDataSo.HeadSprite;
            if (bodyRenderer is not null)
                bodyRenderer.sprite = towerDataSo.BaseSprite;
        }

        private void Update()
        {
            Sensor.Detect();
            currentState.Update(this);
        }

        private void ChangeState(TowerState newState)
        {
            currentState?.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }

        public void ChangeToIdleState() => ChangeState(idleState);
        public void ChangeToShootState() => ChangeState(shootState);
        public void ChangeToRotateState() => ChangeState(rotateState);

        public void StartShooting() => shootRoutine = StartCoroutine(gun.ShootRoutine(1 / towerDataSo.FireRate));
        public void StopShooting()
        {
            if(shootRoutine != null)
                StopCoroutine(shootRoutine);
        }

        public void RotateTowardsTarget()
        {
            if(Sensor.Target == null)   return;
            rotator.Rotate(Sensor.Target, Time.deltaTime);
        }

        public bool IsFacingTarget() => rotator.IsFacingTarget(Sensor.Target);

        private void OnDrawGizmosSelected()
        {
            if (towerDataSo == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,towerDataSo.Range);
        }
    }
}