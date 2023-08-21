
using System;
using System.Linq;
using DGames.Essentials.Editor;
using UnityEditor;
using UnityEngine;
using Editor = UnityEditor.Editor;

namespace DGames.ObjectEssentials.Scriptable.Utils
{
    [CustomEditor(typeof(Values))]
    public partial class ValuesEditor : DGames.Essentials.Editor.Editor
    {
        private SerializedProperty _childrenField;
        private SerializedProperty _keyAndValuesField;
        private string _newChildName;
        private bool _childrenFold;
        private GUIStyle _titleStyle;

        private void OnEnable()
        {
            _childrenField = serializedObject.FindProperty(Values.CHILDREN_FIELD);
            _keyAndValuesField = serializedObject.FindProperty(Values.KEY_VALUE_FIELD);
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();

            CacheIfNeeded();
            EditorGUILayout.BeginHorizontal();
            _childrenFold = EditorGUI.Foldout(EditorGUILayout.GetControlRect(GUILayout.MaxWidth(14)),_childrenFold, "");
            EditorGUILayout.LabelField(GetTitleWithDashes("CHILDREN SECTION"),_titleStyle);
            EditorGUILayout.EndHorizontal();

            var notifiedChanged = false;
            if (_childrenFold)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_childrenField);

                EditorGUILayout.BeginVertical(GUI.skin.box);

                _newChildName = EditorGUILayout.TextField(_newChildName);

                EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(_newChildName));
                EditorGUILayout.Space();
                if (GUILayout.Button("Add Child"))
                {
                    var values = CreateInstance<Values>();
                    values.name = _newChildName;
                    _childrenField.InsertArrayElementAtIndex(_childrenField.arraySize);
                    var property = _childrenField.GetArrayElementAtIndex(_childrenField.arraySize - 1);

                    AssetDatabase.AddObjectToAsset(values, target);
                    AssetDatabase.SaveAssetIfDirty(target);
                    property.objectReferenceValue = values;
                    _newChildName = "";
                    notifiedChanged = true;
                }

                EditorGUI.EndDisabledGroup();

                EditorGUILayout.EndVertical();

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(_keyAndValuesField);
            
            

            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();


            if (notifiedChanged)
            {
                notifiedChanged = false;
                NotifyChanged?.Invoke(this);
            }
        }
        
        private void CacheIfNeeded()
        {
            if (_titleStyle == null)
            {
                var style = EditorStyles.label;
                _titleStyle = new GUIStyle(style) { alignment = TextAnchor.MiddleCenter };
            }
        }
        
        public static string GetTitleWithDashes(string title, int countPerSide = 70)
        {
            var dashes = string.Join("", Enumerable.Repeat("-", countPerSide));

            return $"{dashes}{title}{dashes}";
        }
    }
    
    

    public partial class ValuesEditor : IWindowContent
    {
        public event Action<Essentials.Editor.Editor> NotifyChanged;
    }
}


