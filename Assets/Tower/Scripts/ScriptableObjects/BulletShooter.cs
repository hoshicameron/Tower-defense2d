using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Scripts
{
    [CreateAssetMenu(fileName = "New Bullet Shooter", menuName = "Gameplay/Funcs/Bullet Shooter")]
    public class BulletShooter : ShooterBase
    {
        [field:SerializeField] public GameObject ProjectilePrefab { get; private set; }
        private Coroutine shotRoutine;
        
        private List<Transform> projectileSpawnPoints;

        public void Initialize(List<Transform> projectileSpawnPoints )
        {
            this.projectileSpawnPoints = projectileSpawnPoints;
        }

        public override IEnumerator ShootRoutine(float delay)
        {
            var wait = new WaitForSeconds(delay);
            while (true)
            {
                foreach (var spawnPoint in projectileSpawnPoints)
                {
                    Instantiate(ProjectilePrefab, spawnPoint.position, Quaternion.identity);  
                }

                yield return wait;
            }
        }
    }
}