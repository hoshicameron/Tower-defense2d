using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Movers;
using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Rotators;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.Projectiles
{
    public class Missile : ProjectileBase
    {
        [SerializeField] private TargetMover movingTemplate;
        [SerializeField] private TargetRotator targetRotatorTemplate;
        private TargetMover targetMover;
        private TargetRotator targetRotator;
        public override void Initialize(Transform projectileTarget, Transform projectileSpawnTransform)
        {
            target = projectileTarget;

            targetMover = Instantiate(movingTemplate);
            targetMover.Initialize(transform, ProjectileDataSo.Speed, projectileTarget);

            sensor = Instantiate(sensorTemplate);
            sensor.Initialize(collider,ProjectileDataSo.DetectionLayer,Collide);

            if (targetRotatorTemplate == null) return;
            
            targetRotator = Instantiate(targetRotatorTemplate);
            targetRotator.Initialize(transform,ProjectileDataSo.RotationSpeed);
        }
        protected override void Update()
        {
            if(canMove == false)    return;
            
            targetMover.Move(Time.deltaTime);
            
            sensor.Detect();
            
            if (targetRotator != null) targetRotator.Rotate(target, Time.deltaTime);
        }
    }
}