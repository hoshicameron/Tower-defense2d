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

        public Transform Target { get; set; }
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
        }

        private void Update()
        {
            RotateTowardsTarget();
            HandleShooting();
        }

        private void HandleShooting()
        {
            if (CanShoot() && !isShooting)
                shootRoutine = StartCoroutine(shooterBase.ShootRoutine(1 / towerDataSo.FireRate));
            else if(!CanShoot() && isShooting)
                StopCoroutine(shootRoutine);
        }

        private bool CanShoot()
        {
            if (Target is null)
                return false;
            return Vector2.Distance(Target.position, transform.position) <= towerDataSo.Range;
        }

        private void RotateTowardsTarget()
        {
            if (Target != null)
            {
                rotator.Rotate(Target, Time.deltaTime);
            }
        }
    }
}