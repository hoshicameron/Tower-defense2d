using DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.ScriptableObjects.Datas;
using UnityEditor;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.Editor
{
    [CustomEditor(typeof(TowerBase))]
    public class TowerBaseEditor : UnityEditor.Editor
    {
        private bool showTowerBaseData = true;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var tower = (TowerBase)target;
            EditorGUILayout.Space();
            if(GUILayout.Button("Setup Renderers"))
                tower.SetupRenderer(tower.GetData());

        }
    }
}

