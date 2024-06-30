using UnityEditor;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.ScriptableObjects.Datas.Editor
{
    [CustomEditor(typeof(TowerBaseDataSO))]
    public class TowerBaseDataSOEditor : UnityEditor.Editor
    {
        private TowerBaseDataSO towerBaseDataSo;

        private void OnEnable()
        {
            towerBaseDataSo = target as TowerBaseDataSO;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if(towerBaseDataSo == null)
                return;
            
            var texture = AssetPreview.GetAssetPreview(towerBaseDataSo.BaseSprite);
            GUILayout.Space(10f);
            GUILayout.Label("Sprite Preview:");
            GUILayout.Label("",GUILayout.Height(80f),GUILayout.Width(80f));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(),texture);
        }
    }
}