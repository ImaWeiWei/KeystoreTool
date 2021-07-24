using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools
{
    /// <summary>
    /// keystore 数据结构
    /// </summary>
    [Serializable]
    public class KeystoreData
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string name = "";
        /// <summary>
        /// 路径
        /// </summary>
        public string path = "";
        /// <summary>
        /// 别名
        /// </summary>
        public string alias = "";
        /// <summary>
        /// ks 密码
        /// </summary>
        public string keystorePassword = "";
        /// <summary>
        /// 别名密码
        /// </summary>
        public string aliasPassword = "";
    }
}