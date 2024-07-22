using System;
using DefenseNetwork.CoreTowerDefense.Enums;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using DefenseNetwork.MainSystem.Scripts.ScriptableObjects;
using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.MainSystem.Scripts
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private IntEventChannelSO playerHealthEventChannel;
        [SerializeField] private GameStateEventChannelSO gameStateEventChannel;
        [SerializeField] private VoidEventChannelSO enemyReachedBaseEventChannel;

        [Space] [Header("Data")] 
        [SerializeField] private PlayerDataSO playerData;

        private HealthBehaviour healthBehaviour;
        private void Awake()
        {
            healthBehaviour = ScriptableObject.CreateInstance<HealthBehaviour>();
        }

        private void OnEnable()
        {
            healthBehaviour.onHealthChanged += HealthChanged;
            healthBehaviour.onDeath += PlayerLost;
            enemyReachedBaseEventChannel.OnEventRaised +=TakeDamage;
        }

        private void OnDisable()
        {
            healthBehaviour.onHealthChanged -= HealthChanged;
            healthBehaviour.onDeath -= PlayerLost;
            enemyReachedBaseEventChannel.OnEventRaised -=TakeDamage;
        }

        private void Start()
        {
            healthBehaviour.Initialize(playerData.MaxHealth);
        }

        private void PlayerLost()
        {
            gameStateEventChannel.RaiseEvent(GameState.Lost);
        }

        private void TakeDamage()
        {
            healthBehaviour.TakeDamage(1);
        }

        private void HealthChanged(int currentHealth, int maxHealth) => playerHealthEventChannel.RaiseEvent(currentHealth);
    }
}