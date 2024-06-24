using System.Collections;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Modules.TowerModule.Scripts.Internal
{
    public class Weapon : MonoBehaviour
    {
        public event Action onShoot;
        private Coroutine shootRoutine;

        private IEnumerator ShootRoutine()
        {
            while (true)
            {
                onShoot?.Invoke();
                yield return null;
            }
        }
        
        public void StartShooting()=>shootRoutine = StartCoroutine(ShootRoutine());
        
        public void StopShooting()
        {
            if(shootRoutine != null)
                StopCoroutine(shootRoutine);
        }
    }
}