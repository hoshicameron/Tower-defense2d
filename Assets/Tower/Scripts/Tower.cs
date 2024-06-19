using System;
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

        public Transform Target { get; set; }

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