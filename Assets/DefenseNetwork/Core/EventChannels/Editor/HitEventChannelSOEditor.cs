﻿using DefenseNetwork.Core.EventChannels.DataObjects;
using UnityEditor;

namespace GameSystemsCookbook
{
    [CustomEditor(typeof(HitEventChannelSO))]
    public class HitEventChannelSOEditor : GenericEventChannelSOEditor<HitDTO>
    {
        
    }
}