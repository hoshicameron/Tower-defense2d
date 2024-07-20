using GameSystemsCookbook;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefenseNetwork.Modules.UIModule.Views.GamePlayView.Scripts
{
    public class GamePlayView : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private IntEventChannelSO goldEventChannel;
        [SerializeField] private IntEventChannelSO playerHealthEventChannel;
        [SerializeField] private StringEventChannelSO waveDataEventChannel;
        [SerializeField] private VoidEventChannelSO pauseGameEventChannel;

        [Space] [Header("Header Panel")] 
        [SerializeField] private TextMeshProUGUI playerHealthText;
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private Button pauseButton;

        private void OnEnable()
        {
            goldEventChannel.OnEventRaised += UpdateGoldText;
            playerHealthEventChannel.OnEventRaised += UpdatePlayerHealthText;
            waveDataEventChannel.OnEventRaised += UpdateWaveText;
            
            pauseButton.onClick.AddListener(() => pauseGameEventChannel.RaiseEvent());
        }
        
        private void OnDisable()
        {
            goldEventChannel.OnEventRaised -= UpdateGoldText;
            playerHealthEventChannel.OnEventRaised -= UpdatePlayerHealthText;
            waveDataEventChannel.OnEventRaised -= UpdateWaveText;
        }
        private void UpdateWaveText(string waveString) => waveText.SetText(waveString);
        private void UpdatePlayerHealthText(int playerHealth) => playerHealthText.SetText(playerHealth.ToString());
        private void UpdateGoldText(int goldAmount) => goldText.SetText(goldAmount.ToString());
        
        
    }
}