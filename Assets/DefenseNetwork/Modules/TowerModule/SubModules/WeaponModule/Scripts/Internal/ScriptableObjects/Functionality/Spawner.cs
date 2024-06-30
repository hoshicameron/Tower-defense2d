using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.Projectiles;
using UnityEngine;

namespace Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Functionality
{
    public class Spawner : ScriptableObject
    {
        private List<Transform> projectileSpawnPoints;
        private Projectile projectilePrefab;

        public void Initialize(List<Transform> spawnPoints, Projectile projectileToSpawn )
        {
            projectileSpawnPoints = spawnPoints;
            projectilePrefab = projectileToSpawn;
        }

        public IEnumerator ShootRoutine(Transform target, float delay)
        {
            var wait = new WaitForSeconds(delay);
            while (true)
            {
                foreach (var newProjectile in 
                         projectileSpawnPoints.Select(spawnPoint => 
                             Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity)))
                {
                    newProjectile.Initialize(target);
                }

                yield return wait;
            }
        }
        
    }
}