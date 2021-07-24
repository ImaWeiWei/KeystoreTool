using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace MyTools
{

    [CustomEditor(typeof(KeystoreConfigData))]
    public class KeystoreConfigInspector : Editor
    {
        private ReorderableList _reorderableList;
        /// <summary>
        /// 下拉菜单项
        /// </summary>
        private List<string> _options;

        private int _optionIndex;


        private void OnEnable()
        {
            InitOptions();

            _reorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("keystorelist"), true, true, true, true);

            _reorderableList.elementHeight = EditorGUIUtility.singleLineHeight * 4 + 10f * 3;

            _reorderableList.drawHeaderCallback = (Rect rect) =>
            {
                GUI.Label(rect, "Keystores");
            };

            _reorderableList.drawElementCallback = (Rect rect, int index, bool selected, bool focused) =>
            {
                SerializedProperty item = _reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                EditorGUI.PropertyField(rect, item);
            };

            _reorderableList.onAddCallback = (ReorderableList list) =>
            {
                if (list.serializedProperty != null)
                {
                    list.serializedProperty.arraySize++;
                    list.index = list.serializedProperty.arraySize - 1;
                    SerializedProperty item = list.serializedProperty.GetArrayElementAtIndex(list.index);
                    var name = item.FindPropertyRelative("name");
                    name.stringValue = $"Keystore_{ list.index}";
                    _options.Add(name.stringValue);
                }
                else
                {
                    ReorderableList.defaultBehaviours.DoAddButton(list);
                }
            };

            _reorderableList.onRemoveCallback = (ReorderableList list) =>
            {
                if (EditorUtility.DisplayDialog("警告", "是否删除元素?", "删除", "取消"))
                {
                    SerializedProperty item = list.serializedProperty.GetArrayElementAtIndex(list.index);
                    var name = item.FindPropertyRelative("name");
                    _options.Remove(name.stringValue);
                    ReorderableList.defaultBehaviours.DoRemoveButton(list);
                }
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawPopup();
            _reorderableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawPopup()
        {
            EditorGUI.BeginChangeCheck();
            _optionIndex = EditorGUILayout.Popup(_optionIndex, _options.ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                var defaultIndex = serializedObject.FindProperty("defaultIndex");
                defaultIndex.intValue = _optionIndex;
            }
        }

        private void InitOptions()
        {
            var defaultIndex = serializedObject.FindProperty("defaultIndex");
            _optionIndex = defaultIndex.intValue;
            _options = new List<string>();
            var list = serializedObject.FindProperty("keystorelist");
            for (int i = 0; i < list.arraySize; i++)
            {
                var property = list.GetArrayElementAtIndex(i);
                var name = property.FindPropertyRelative("name");
                _options.Add(name.stringValue);
            }
        }
    }
}
