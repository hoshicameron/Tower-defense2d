#nullable enable
using System;
using DefenseNetwork.CoreTowerDefense.Enums;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.ScriptableObjects.Datas;
using UnityEngine;
using UnityEngine.Events;

namespace DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal
{
    [RequireComponent(typeof(TowerSensorProxy))]
    public class TowerBase : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private GameStateEventChannelSO gameStateEventChannel;
        
        [Header("Components")]
        [SerializeField] private SpriteRenderer bodyRenderer;
        
        [Space][Header("Events")]
        [SerializeField] public UnityEvent<Transform?> onTargetDetected;

        [Space][Header("Data")]
        [SerializeField] private TowerBaseDataSO towerBaseDataSo;

        private TowerSensorProxy towerSensorProxy;

        private void Awake()
        {
            towerSensorProxy = GetComponent<TowerSensorProxy>();
        }

        private void OnEnable()
        {
            gameStateEventChannel.OnEventRaised += HandleGameStateChange;
        }
        
        private void OnDisable()
        {
            gameStateEventChannel.OnEventRaised -= HandleGameStateChange;
        }

        private void HandleGameStateChange(GameState state)
        {
            switch (state)
            {
                case GameState.Playing:
                    EnableSensor();
                    break;
                case GameState.Paused:
                case GameState.Won:
                case GameState.Lost:
                    DisableSensor();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        

        private void DisableSensor() => towerSensorProxy.enabled = false;

        private void EnableSensor() => towerSensorProxy.enabled = true;

        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            SetupRenderer(towerBaseDataSo);
            towerSensorProxy.Initialize(towerBaseDataSo.DetectionLayer,TargetDetected);
            
        }
        private void OnValidate()
        {
            ValidateTowerBaseData(towerBaseDataSo);
        }

        private void ValidateTowerBaseData(TowerBaseDataSO data)
        {
            SetupRenderer(data);
        }

        public void SetupRenderer(TowerBaseDataSO data)
        {
            if (bodyRenderer !=null && data != null)
                bodyRenderer.sprite = data.BaseSprite;
        }

        private void TargetDetected(Transform? target)
        {
            onTargetDetected?.Invoke(target);
        }
        public TowerBaseDataSO GetData() => towerBaseDataSo;
    }
}