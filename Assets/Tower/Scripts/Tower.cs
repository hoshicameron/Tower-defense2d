using UnityEngine;

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
        [SerializeField] private ShooterBase shooterBase;
        [SerializeField] private Sensor sensor;

        private Transform target;
        private bool isShooting;
        private Coroutine shootRoutine;

        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            if (headRenderer is not null)
                headRenderer.sprite = towerDataSo.HeadSprite;
            if (bodyRenderer is not null)
                bodyRenderer.sprite = towerDataSo.BaseSprite;
            rotator.Initialize(head, towerDataSo.TurnSpeed);
            sensor.Initialize(transform,towerDataSo.Range,towerDataSo.DetectionLayer,EnemyDetected);
        }

        private void EnemyDetected(Transform detectedTarget)
        {
            target = detectedTarget;
        }

        private void Update()
        {
            sensor.Detect();
            
            if(target is null)
                return;
            
            RotateTowardsTarget();
            HandleShooting();
        }

        private void HandleShooting()
        {
            if (isShooting)
            {
                shootRoutine = StartCoroutine(shooterBase.ShootRoutine(1 / towerDataSo.FireRate));
                isShooting = true;
            }
            else if (isShooting)
            {
                StopCoroutine(shootRoutine);
                isShooting = false;
            }
        }

       
        private void RotateTowardsTarget()
        {
            rotator.Rotate(target, Time.deltaTime);
        }
    }
}