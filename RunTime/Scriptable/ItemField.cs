using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DGames.ObjectEssentials.Scriptable
{
    [Serializable]
    public abstract class ItemField<TItem,TInterfaceItem>
    {
        [SerializeField] protected Type type;
        [SerializeField] protected string key;
        [SerializeField] protected TItem item;



        public abstract TInterfaceItem Item { get; }


        protected ItemField(string key)
        {
            type = Type.Key;
            this.key = key;
            item = default;
        }

        public enum Type
        {
            Key,
            Scriptable,
        }
    }
    
    #if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(ItemField<,>),true)]
    public class ItemFieldPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.LabelField(position, label);
            var typeProperty = property.FindPropertyRelative("type");
            var keyProperty = property.FindPropertyRelative("key");
            var itemProperty = property.FindPropertyRelative("item");

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
                : itemProperty;

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