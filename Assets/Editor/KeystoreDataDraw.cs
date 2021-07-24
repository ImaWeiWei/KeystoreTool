using UnityEditor;
using UnityEngine;

namespace MyTools
{
    [CustomPropertyDrawer(typeof(KeystoreData))]
    public class KeystoreDataDraw : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //创建一个属性包装器，用于将常规GUI控件与SerializedProperty一起使用
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                EditorGUIUtility.labelWidth = 60;
                position.height = EditorGUIUtility.singleLineHeight;

                var path = property.FindPropertyRelative("path");
                var alias = property.FindPropertyRelative("alias");
                var keystorePassword = property.FindPropertyRelative("keystorePassword");
                var aliasPassword = property.FindPropertyRelative("aliasPassword");

                var rect = new Rect(position) { width = 400 };

                GUI.enabled = false;
                path.stringValue = EditorGUI.TextField(rect, "路径", path.stringValue);
                GUI.enabled = true;
                if (GUI.Button(new Rect(position) { x = rect.x + rect.width + 20, width = 50 }, "打开"))
                {
                    string temp = EditorUtility.OpenFilePanel("keystore", Application.dataPath, "keystore");
                    if (!string.IsNullOrEmpty(temp))
                    {
                        path.stringValue = temp;
                    }
                }
                keystorePassword.stringValue = EditorGUI.TextField(new Rect(rect) { y = rect.y + (EditorGUIUtility.singleLineHeight + 2.5f) }, "密码", keystorePassword.stringValue);
                alias.stringValue = EditorGUI.TextField(new Rect(rect) { y = rect.y + (EditorGUIUtility.singleLineHeight + 2.5f) * 2 }, "别名", alias.stringValue);
                aliasPassword.stringValue = EditorGUI.TextField(new Rect(rect) { y = rect.y + (EditorGUIUtility.singleLineHeight + 2.5f) * 3 }, "别名密码", aliasPassword.stringValue);
            }
        }
    }
}
