#nullable enable
using DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.ScriptableObjects.Datas;
using UnityEngine;
using UnityEngine.Events;

namespace DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal
{
    [RequireComponent(typeof(TowerSensorProxy))]
    public class TowerBase : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private SpriteRenderer bodyRenderer;
        
        /*[Space][Header("Behaviours")]
        [SerializeField] private TowerSensor towerSensorTemplate;*/
        
        [Space][Header("Events")]
        [SerializeField] public UnityEvent<Transform?> onTargetDetected;

        [Space][Header("Data")]
        [SerializeField] private TowerBaseDataSO towerBaseDataSo;

        private TowerSensorProxy towerSensorProxy;

        private void Awake()
        {
            towerSensorProxy = GetComponent<TowerSensorProxy>();
        }

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