using System.Collections;
using System.Collections.Generic;
using DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.Projectiles;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality
{
    public class ProjectileSpawner : ScriptableObject
    {
        private List<Transform> projectileSpawnPoints;
        private ProjectileBase bulletPrefab;
        private float delay;
        private Transform target;

        public void Initialize(List<Transform> spawnPoints, ProjectileBase bulletToSpawn,float ShootDelay )
        {
            projectileSpawnPoints = spawnPoints;
            bulletPrefab = bulletToSpawn;
            delay = ShootDelay;
        }

        public void SetTarget(Transform targetTosShoot) => target = targetTosShoot;
        public IEnumerator ShootRoutine()
        {
            var wait = new WaitForSeconds(delay);
            while (true)
            {
                foreach (var spawnPoint in projectileSpawnPoints)
                {
                    var newProjectile = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
                    newProjectile.Initialize(target, spawnPoint);
                }

                yield return wait;
            }
        }
        
    }
}