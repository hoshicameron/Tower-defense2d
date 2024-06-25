using Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal;
using UnityEditor;
using UnityEngine;

namespace Modules.TowerModule.Scripts.Internal.ScriptableObjects.Datas.Editor
{
    [CustomEditor(typeof(TowerBase))]
    public class TowerEditor : UnityEditor.Editor
    {
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