﻿using System;
using DefenseNetwork.Modules.UIModule.GamePlay.TowerSpotView.Scripts.Enums;
using GameSystemsCookbook;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefenseNetwork.Modules.UIModule.GamePlay.TowerSpotView.Scripts
{
    [RequireComponent(typeof(Button))]
    public class TowerDeployButton : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private IntEventChannelSO pointUpdateEventChannel;
        [Header("UI")]
        [SerializeField] private Image towerImage;
        [SerializeField] private TextMeshProUGUI costText;
        
        private Button button;
        private TowerType towerDeployType;
        private int towerDeployCost;

        public event Action<int, int> onClick;
        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            pointUpdateEventChannel.OnEventRaised += PointUpdated;
        }

        private void OnDisable()
        {
            pointUpdateEventChannel.OnEventRaised -= PointUpdated;
        }

        private void Start()
        {
            button.onClick.AddListener(()=>onClick?.Invoke((int)towerDeployType, int.Parse(costText.text)));
        }

        private void PointUpdated(int point)
        {
            button.interactable = towerDeployCost >= point;
        }

        public void UpdateUI(Sprite towerSprite, int cost, TowerType towerType)
        {
            towerImage.sprite = towerSprite;
            towerDeployCost = cost;
            costText.SetText(cost.ToString());
            towerDeployType = towerType;
        }
    }
}