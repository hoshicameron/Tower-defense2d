using UnityEditor;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Data.Editor
{
    [CustomEditor(typeof(ProjectileDataSO))]
    public class ProjectileDataSOEditor : UnityEditor.Editor
    {
        private ProjectileDataSO projectileDataSo;

        private void OnEnable()
        {
            projectileDataSo = target as ProjectileDataSO;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (projectileDataSo == null)
                return;
            var texture = AssetPreview.GetAssetPreview(projectileDataSo.Sprite);
            GUILayout.Space(10f);
            GUILayout.Label("Sprite Preview:");
            GUILayout.Label("",GUILayout.Height(80f),GUILayout.Width(80f));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(),texture);
        }
    }
}