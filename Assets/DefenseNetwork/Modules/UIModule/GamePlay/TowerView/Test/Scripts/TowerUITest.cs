using System.Collections.Generic;
using System.Linq;
using DefenseNetwork.Modules.TowerModule.Scripts.Scripts;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.UIModule.GamePlay.TowerView.Test.Scripts
{
    public class TowerUITest : MonoBehaviour
    {
        [SerializeField] private IntEventChannelSO towerSoldChannel;
        [SerializeField] private List<Tower> towers;
        [SerializeField] private int currentScore = 100;

        private Dictionary<GameObject, Tower> gameObjectTowerDictionary;

        private void OnEnable()
        {
            towerSoldChannel.OnEventRaised += UpdateScore;
        }

        private void OnDisable()
        {
            towerSoldChannel.OnEventRaised -= UpdateScore;
        }
    
        private void Start()
        {
            foreach (var tower in towers.Where(tower => !gameObjectTowerDictionary.ContainsKey(tower.gameObject)))
            {
                gameObjectTowerDictionary.Add(tower.gameObject,tower);
            }
        }

        private void UpdateScore(int income) => currentScore += income;

    

    
    }
}
