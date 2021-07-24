using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 需求：
/// - 打开工程时，会自动检测keystore配置，并填入相应keystore
/// - 创建一个keystore的配置
///     - 可配置多个keystores
///         - 路径
///         - 别名
///         - 密码
///         - 别名密码
///- 加载指定的keystore配置
/// </summary>

namespace MyTools
{
    /// <summary>
    /// keystore 管理类
    /// </summary>
    [InitializeOnLoad]
    public static class KeystoreManager
    {
        public const string KEY_CONFIG_PATH = "MyTools_KeystoreConfig";

        static KeystoreManager()
        {
            if (!string.IsNullOrEmpty(PlayerSettings.Android.keystorePass)) { return; }
            LoadKeystoreConfig();
        }

        /// <summary>
        /// 启动项目时加载配置
        /// </summary>
        [MenuItem("MyTools/加载Keystore配置", false, 2)]
        private static void LoadKeystoreConfig()
        {
            string path = EditorPrefs.GetString(KEY_CONFIG_PATH, "");
            if (string.IsNullOrEmpty(path))
            {
                EditorUtility.DisplayDialog("错误", $"configPath:\"{path}\" 不存在", "确定");
            }
            else
            {
                var data = AssetDatabase.LoadAssetAtPath<KeystoreConfigData>(path);
                if (data.defaultIndex >= 0)
                {
                    KeystoreData keystore = data.keystorelist[data.defaultIndex];
                    if (keystore != null)
                    {
                        PlayerSettings.Android.useCustomKeystore = true;
                        PlayerSettings.Android.keystoreName = keystore.path;
                        PlayerSettings.Android.keystorePass = keystore.keystorePassword;
                        PlayerSettings.Android.keyaliasName = keystore.alias;
                        PlayerSettings.Android.keyaliasPass = keystore.aliasPassword;
                        Debug.Log("设置keystore成功");
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog("错误", "没有找到keystore数据", "确定");
                }
            }
        }

        /// <summary>
        /// 创建keystore config 配置
        /// </summary>
        [MenuItem("MyTools/创建Keystore配置", false, 1)]
        private static void CreateKeystoreConfig()
        {
            KeystoreConfigData config = ScriptableObject.CreateInstance<KeystoreConfigData>();

            string path = EditorUtility.SaveFilePanelInProject("保存", "KeystoreConfig", "asset", "确定");
            if (!string.IsNullOrEmpty(path))
            {
                EditorPrefs.SetString(KEY_CONFIG_PATH, path);
                AssetDatabase.CreateAsset(config, path);
                AssetDatabase.Refresh();
            }
            else
            {
                EditorUtility.DisplayDialog("错误", $"路径错误:\"{path}\" 错误", "确定");
            }
        }
    }
}
