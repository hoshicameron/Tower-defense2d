using System.Collections;
using UnityEngine;

namespace Tower.Scripts
{
    public abstract class ShooterBase : ScriptableObject
    {
        public abstract IEnumerator ShootRoutine(float delay);
    }
}