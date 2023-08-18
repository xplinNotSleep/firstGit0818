using System.Collections;
using System.IO;
using System.Resources;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// ��Դ�ļ�������
    /// </summary>
    public class ResourceHelper
    {
        private string m_FilePath;
        private Hashtable m_Hashtable;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="filepath">��Դ�ļ�·��</param>
        public ResourceHelper(string filepath)
        {
            this.m_FilePath = filepath;
            this.m_Hashtable = new Hashtable();

            //�������
            if (File.Exists(filepath))
            {
                string tempFile = CommonConstString.STR_TempPath + "\\decryptFile.resx";

                //�����ļ�
                Security tSecurity = new Security();
                tSecurity.DecryptDES(filepath, tempFile);

                using (ResourceReader ResReader = new ResourceReader(tempFile))
                {
                    IDictionaryEnumerator tDictEnum = ResReader.GetEnumerator();
                    while (tDictEnum.MoveNext())
                    {
                        try
                        {
                            //�п���û�����͵��·����л�����catchס���ô���
                            this.m_Hashtable.Add(tDictEnum.Key, tDictEnum.Value);
                        }
                        catch { }
                    }

                    ResReader.Close();
                }

                //ɾ����ʱ�ļ�
                File.Delete(tempFile);
            }
        }

        /// <summary>
        /// ��ȡ�����ù�ϣ���ֵ
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
        /// ��ȡָ���ؼ��ֵ��ַ�������
        /// </summary>
        /// <param name="pResName">�ؼ���</param>
        /// <returns>���������ָ���Ĺؼ����򷵻�""</returns>
        public string GetString(string pResName)
        {
            object tObj = GetObject(pResName);
            return (tObj == null) ? "" : tObj.ToString();
        }

        /// <summary>
        /// ��ȡָ���ؼ��ֶ����ֵ
        /// </summary>
        /// <param name="pResName">�ؼ���</param>
        /// <returns>���������ָ���ؼ��ֶ����򷵻�null.</returns>
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
        /// ���ָ���ؼ��ּ���ֵ����(������������)
        /// </summary>
        /// <param name="pResName">�ؼ���</param>
        /// <param name="pObj">Objectֵ</param>
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
        /// ���ָ���ؼ��ּ���ֵ����(������������)
        /// </summary>
        /// <param name="pResName">�ؼ���</param>
        /// <param name="pStr">�ַ���ֵ</param>
        public void SetString(string pResName, string pStr)
        {
            SetObject(pResName, pStr);
        }

        /// <summary>
        /// ɾ��ָ���ؼ��ֵĶ���
        /// </summary>
        /// <param name="pResName">�ؼ�������</param>
        public void DeleteObject(string pResName)
        {
            if (m_Hashtable.ContainsKey(pResName) == true)
            {
                this.m_Hashtable.Remove(pResName);
            }
        }

        /// <summary>
        /// �����ԭ����Դ�ļ����޸�
        /// </summary>
        public void Save()
        {
            SaveAs(this.m_FilePath);
        }

        /// <summary>
        /// ���Ϊָ��·������Դ�ļ�
        /// </summary>
        /// <param name="filepath">ָ�����ļ�·��</param>
        public void SaveAs(string filepath)
        {
            if (filepath.Length == 0) return;

            //�����������ɾ��
            if (File.Exists(filepath)) File.Delete(filepath);

            string tempFile = CommonConstString.STR_TempPath + "\\encryFile.resx";
            //--���� �޸�----2020/12/31 ·�������ڻᱨ��
            if (!Directory.Exists(CommonConstString.STR_TempPath))
            {
                DirectoryInfo info = Directory.CreateDirectory(CommonConstString.STR_TempPath);
                if (!info.Exists)
                {
                    return;
                }
            }
            using (ResourceWriter ResWriter = new ResourceWriter(tempFile))
            {
                foreach (DictionaryEntry tDictEntry in this.m_Hashtable)
                {
                    //�����Դ��
                    ResWriter.AddResource(tDictEntry.Key.ToString(), tDictEntry.Value);
                }

                //��������Դ��ϵͳĬ�ϸ�ʽ�������
                ResWriter.Generate();

                ResWriter.Close();
            }

            //�ļ�����
            Security tSecurity = new Security();
            tSecurity.EncryptDES(tempFile, filepath);

            //ɾ����ʱ�ļ�
            File.Delete(tempFile);
        }
    }
}
