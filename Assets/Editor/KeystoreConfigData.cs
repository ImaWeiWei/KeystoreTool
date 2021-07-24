using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools
{
    /// <summary>
    /// keystore config 数据结构
    /// </summary>
    [Serializable]
    public class KeystoreConfigData : ScriptableObject
    {
        /// <summary>
        /// keystore集合
        /// </summary>
        public List<KeystoreData> keystorelist = new List<KeystoreData>();
        /// <summary>
        /// 默认
        /// </summary>
        public int defaultIndex = -1;
    }
}
