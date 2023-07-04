
using UnityEditor;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable.Utils
{
    [CustomEditor(typeof(Values))]
    public class ValuesEditor : Editor
    {
        private SerializedProperty _childrenField;
        private SerializedProperty _keyAndValuesField;
        private string _newChildName;
        private bool _childrenFold;

        private void OnEnable()
        {
            _childrenField = serializedObject.FindProperty(Values.CHILDREN_FIELD);
            _keyAndValuesField = serializedObject.FindProperty(Values.KEY_VALUE_FIELD);
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();

            _childrenFold = EditorGUILayout.Foldout(_childrenFold, "----------CHILDREN SECTION---------");

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
                }

                EditorGUI.EndDisabledGroup();

                EditorGUILayout.EndVertical();

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(_keyAndValuesField);
            
            

            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
        }
    }
}


