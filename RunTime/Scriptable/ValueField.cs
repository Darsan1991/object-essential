using System;
using DGames.ObjectEssentials.Scriptable.Utils;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DGames.ObjectEssentials.Scriptable
{
    [Serializable]
    public struct ValueField<T>
    {
        [SerializeField] private Type _type;
        [SerializeField] private string _key;
        [SerializeField] private Value<T> _value;


        public static implicit operator T(ValueField<T> field) => field.Value.Get();

        public IValue<T> Value => _type switch
        {
            Type.Key => Values.Default.Get<T>(_key),
            Type.Scriptable => _value,
            _ => throw new ArgumentOutOfRangeException()
        };


        public ValueField(string key)
        {
            _type = Type.Key;
            _key = key;
            _value = null;
        }

        public enum Type
        {
            Key,
            Scriptable,
        }
    }


    // --------------------------------------------------------- 
    // --------------------------------------------------------- 

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(ValueField<>))]
    public class ValueFieldPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.LabelField(position, label);
            var typeProperty = property.FindPropertyRelative("_type");
            var keyProperty = property.FindPropertyRelative("_key");
            var valueProperty = property.FindPropertyRelative("_value");

            var leftWidth = position.width;
            position.position += Vector2.right * EditorGUIUtility.labelWidth;

            leftWidth -= EditorGUIUtility.labelWidth;
            position.width = 10;

            if (EditorGUI.DropdownButton(position, EditorGUIUtility.IconContent("SettingsIcon"), FocusType.Passive,
                    new GUIStyle { border = new RectOffset(1, 1, 1, 1) , alignment = TextAnchor.MiddleCenter}))
            {
                ShowPopupForType(typeProperty);
            }

            position.x += position.width+3;
            leftWidth -= position.width+3;
            position.width = leftWidth;

            var targetProperty = typeProperty.enumValueIndex == (int)ValueField<int>.Type.Key
                ? keyProperty
                : valueProperty;

            EditorGUI.PropertyField(position, targetProperty, GUIContent.none);
            EditorGUI.EndProperty();
        }

        private void ShowPopupForType(SerializedProperty typeProperty)
        {
            var menu = new GenericMenu();
            foreach (ValueField<int>.Type value in Enum.GetValues(typeof(ValueField<int>.Type)))
            {
                menu.AddItem(new GUIContent(value.ToString()), (int)value == typeProperty.enumValueIndex,
                    () =>
                    {
                        typeProperty.enumValueIndex = (int)value;
                        typeProperty.serializedObject.ApplyModifiedProperties();
                    });
            }


            menu.ShowAsContext();
        }
    }

#endif
}