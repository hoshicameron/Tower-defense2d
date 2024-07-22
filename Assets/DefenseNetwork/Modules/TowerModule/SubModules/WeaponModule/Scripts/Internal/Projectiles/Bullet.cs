using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects.Movers;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.Projectiles
{
    public class Bullet : ProjectileBase
    {
        [Space]
        [Header("Behaviours")]
        [SerializeField] private DirectionalMover movingTemplate;
        private DirectionalMover directionalMover;
        public override void Initialize(Transform projectileTarget, Transform projectileSpawnTransform)
        {
            target = projectileTarget;
            directionalMover = Instantiate(movingTemplate);
            directionalMover.Initialize(transform, ProjectileDataSo.Speed);
            directionalMover.SetDirection(projectileSpawnTransform.up);

            sensor = Instantiate(sensorTemplate);
            sensor.Initialize(collider,ProjectileDataSo.DetectionLayer,Collide);
        }
        
        protected override void Update()
        {
            if(canMove == false)    return;
            
            directionalMover.Move(Time.deltaTime);
            sensor.Detect();
        }
    }
}