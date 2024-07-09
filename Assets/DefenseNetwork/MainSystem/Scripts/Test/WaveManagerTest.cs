using System;
using UnityEngine;

namespace DefenseNetwork.MainSystem.Scripts.Test
{
    public class WaveManagerTest : MonoBehaviour
    {
        [SerializeField] private WaveManager waveManager;

        private void Start()
        {
            waveManager.StartNextWave();
        }
    }
}