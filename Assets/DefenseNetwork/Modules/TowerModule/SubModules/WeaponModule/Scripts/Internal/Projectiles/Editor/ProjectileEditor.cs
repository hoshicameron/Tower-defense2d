using DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.ScriptableObjects.Data;
using UnityEditor;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.Projectiles.Editor
{
    [CustomEditor(typeof(Bullet))]
    public class ProjectileEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var projectile = (Bullet)target;

            if (projectile.ProjectileDataSo == null) return;
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Projectile Data Properties", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();

            var lifeTime = EditorGUILayout.FloatField("Life Time", projectile.ProjectileDataSo.LifeTime);
            var speed = EditorGUILayout.FloatField("Speed", projectile.ProjectileDataSo.Speed);
            var damage = EditorGUILayout.IntField("Damage", projectile.ProjectileDataSo.Damage);
            var rotationSpeed =
                EditorGUILayout.FloatField("Rotation Speed", projectile.ProjectileDataSo.RotationSpeed);
            var sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", projectile.ProjectileDataSo.Sprite,
                typeof(Sprite), false);

            if (!EditorGUI.EndChangeCheck()) return;
                
            Undo.RecordObject(projectile.ProjectileDataSo, "Modified Projectile Data");

            typeof(ProjectileDataSO).GetProperty("LifeTime")?.SetValue(projectile.ProjectileDataSo, lifeTime);
            typeof(ProjectileDataSO).GetProperty("Speed")?.SetValue(projectile.ProjectileDataSo, speed);
            typeof(ProjectileDataSO).GetProperty("Damage")?.SetValue(projectile.ProjectileDataSo, damage);
            typeof(ProjectileDataSO).GetProperty("RotationSpeed")
                ?.SetValue(projectile.ProjectileDataSo, rotationSpeed);
            typeof(ProjectileDataSO).GetProperty("Sprite")?.SetValue(projectile.ProjectileDataSo, sprite);

            EditorUtility.SetDirty(projectile.ProjectileDataSo);

                    
            projectile.SendMessage("OnValidate", SendMessageOptions.DontRequireReceiver);
        }
    }
}