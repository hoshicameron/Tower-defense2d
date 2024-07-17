﻿using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This event channel broadcasts and carries TowerUpgradeRequest payload.
    /// </summary>
    [CreateAssetMenu(fileName = "TowerModificationRequestEventChannelSO", menuName = "Events/TowerModificationRequestEventChannelSO")]
    public class TowerModificationRequestEventChannelSO : GenericEventChannelSO<TowerModificationRequest>
    {
        
    }
}