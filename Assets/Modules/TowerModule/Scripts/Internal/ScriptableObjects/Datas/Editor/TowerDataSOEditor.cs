using System;
using UnityEditor;
using UnityEngine;

namespace Modules.TowerModule.Scripts.Internal.ScriptableObjects.Datas.Editor
{
    [CustomEditor(typeof(TowerDataSO))]
    public class TowerDataSOEditor : UnityEditor.Editor
    {
        private TowerDataSO towerDataSo;

        private void OnEnable()
        {
            towerDataSo = target as TowerDataSO;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(towerDataSo == null)
                return;
            
            var texture = AssetPreview.GetAssetPreview(towerDataSo.BaseSprite);
            GUILayout.Space(10f);
            GUILayout.Label("Sprite Preview:");
            GUILayout.Label("",GUILayout.Height(80f),GUILayout.Width(80f));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(),texture);
        }
    }
}