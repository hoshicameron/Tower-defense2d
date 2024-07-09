using UnityEditor;
using UnityEngine;

namespace DefenseNetwork.Modules.EnemyModule.Scripts.ScriptableObjects.Data.Editor
{
    [CustomEditor(typeof(EnemyDataSO))]
    public class EnemyDataSOEditor : UnityEditor.Editor
    {
        private EnemyDataSO enemyDataSo;

        private void OnEnable()
        {
            enemyDataSo = target as EnemyDataSO;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if(enemyDataSo == null) return;
            
            var texture = AssetPreview.GetAssetPreview(enemyDataSo.Sprite);
            GUILayout.Space(10f);
            GUILayout.Label("Sprite Preview:");
            GUILayout.Label("",GUILayout.Height(80f),GUILayout.Width(80f));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(),texture);
        }
    }
}