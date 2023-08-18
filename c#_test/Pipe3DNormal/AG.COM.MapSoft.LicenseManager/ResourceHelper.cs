/************************************************************************************
 * Copyright (c) 2016  All Rights Reserved.
 *命名空间：AG.COM.MapSoft.LicenseManager
 *文件名：  ResourceHelper
 *创建人：  胡仁勇
 *创建时间：2016-11-21 10:01:21
 *描述
 *=====================================================================
 *修改标记
 *修改时间：2016-11-21 10:01:21
 *修改人： 胡仁勇
 *描述：
************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.COM.MapSoft.Tool;
using System.Collections;
using System.IO;
using System.Resources;
namespace AG.COM.MapSoft.LicenseManager
{
    /// <summary>
    /// 资源文件帮助类
    /// </summary>
    public class ResourceHelper
    {
        private string m_FilePath;
        private Hashtable m_Hashtable;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="filepath">资源文件路径</param>
        public ResourceHelper(string filepath)
        {
            this.m_FilePath = filepath;
            this.m_Hashtable = new Hashtable();

            //如果存在
            if (File.Exists(filepath))
            {
                string tempFile = CommonConstString.STR_ConfigPath + "decryptFile.resx";

                //解密文件
                Security tSecurity = new Security();
                tSecurity.DecryptDES(filepath, tempFile);

                using (ResourceReader ResReader = new ResourceReader(tempFile))
                {
                    IDictionaryEnumerator tDictEnum = ResReader.GetEnumerator();
                    while (tDictEnum.MoveNext())
                    {
                        this.m_Hashtable.Add(tDictEnum.Key, tDictEnum.Value);
                    }

                    ResReader.Close();
                }

                //删除临时文件
                File.Delete(tempFile);
            }
        }

        /// <summary>
        /// 获取或设置哈希表键值
        /// </summary>
        public Hashtable KeyValues
        {
            get
            {
                return this.m_Hashtable;
            }
            set
            {
                this.m_Hashtable = value;
            }
        }

        /// <summary>
        /// 获取指定关键字的字符串对象
        /// </summary>
        /// <param name="pResName">关键字</param>
        /// <returns>如果不存在指定的关键字则返回""</returns>
        public string GetString(string pResName)
        {
            object tObj = GetObject(pResName);
            return (tObj == null) ? "" : tObj.ToString();
        }

        /// <summary>
        /// 获取指定关键字对象的值
        /// </summary>
        /// <param name="pResName">关键字</param>
        /// <returns>如果不存在指定关键字对象则返回null.</returns>
        public object GetObject(string pResName)
        {
            string strKeyUpper = pResName.ToUpper();
            if (this.m_Hashtable.ContainsKey(strKeyUpper))
            {
                return this.m_Hashtable[strKeyUpper];
            }
            else
                return null;
        }

        /// <summary>
        /// 添加指定关键字及其值对象(如果存在则更新)
        /// </summary>
        /// <param name="pResName">关键字</param>
        /// <param name="pObj">Object值</param>
        public void SetObject(string pResName, object pObj)
        {
            string strKeyUpper = pResName.ToUpper();
            if (this.m_Hashtable.ContainsKey(strKeyUpper))
            {
                this.m_Hashtable[strKeyUpper] = pObj;
            }
            else
            {
                this.m_Hashtable.Add(strKeyUpper, pObj);
            }
        }

        /// <summary>
        /// 添加指定关键字及其值对象(如果存在则更新)
        /// </summary>
        /// <param name="pResName">关键字</param>
        /// <param name="pStr">字符串值</param>
        public void SetString(string pResName, string pStr)
        {
            SetObject(pResName, pStr);
        }

        /// <summary>
        /// 删除指定关键字的对象
        /// </summary>
        /// <param name="pResName">关键字名称</param>
        public void DeleteObject(string pResName)
        {
            if (m_Hashtable.ContainsKey(pResName) == true)
            {
                this.m_Hashtable.Remove(pResName);
            }
        }

        /// <summary>
        /// 保存对原有资源文件的修改
        /// </summary>
        public void Save()
        {
            SaveAs(this.m_FilePath);
        }

        /// <summary>
        /// 另存为指定路径的资源文件
        /// </summary>
        /// <param name="filepath">指定的文件路径</param>
        public void SaveAs(string filepath)
        {
            if (filepath.Length == 0) return;

            //如果存在则先删除
            if (File.Exists(filepath)) File.Delete(filepath);

            string tempFile = CommonConstString.STR_ConfigPath + "\\encryFile.resx";

            using (ResourceWriter ResWriter = new ResourceWriter(tempFile))
            {
                foreach (DictionaryEntry tDictEntry in this.m_Hashtable)
                {
                    //添加资源对
                    ResWriter.AddResource(tDictEntry.Key.ToString(), tDictEntry.Value);
                }

                //将所有资源以系统默认格式输出到流
                ResWriter.Generate();

                ResWriter.Close();
            }

            //文件加密
            Security tSecurity = new Security();
            tSecurity.EncryptDES(tempFile, filepath);

            //删除临时文件
            File.Delete(tempFile);
        }
    }
}
