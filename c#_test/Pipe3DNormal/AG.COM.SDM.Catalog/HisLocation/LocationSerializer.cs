using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// �ռ�����λ�����л�
    /// </summary>
    public class LocationSerializer
    {
        /// <summary>
        /// ����ָ����������Ϊ�ַ���
        /// </summary>
        /// <param name="item">������</param>
        /// <returns>�����ַ���</returns>
        public static string SaveToString(DataItem item)
        {
            //�����ʽ
            //�ļ���,�����ռ��ļ�,FeatureDataset��
            //�����sde���ӣ����ļ���Ϊ��
            //�����ռ��ļ�������.sde��.mdb��.gdb��׺��.gdb��׺�����ļ��У���û��Ӱ�죩��
            if (item is FolderItem)
            {
                return (item as FolderItem).FileName;
            }
            else
            {
                string s1 = "", s2 = "", s3 = "";
                if (item is DatabaseConnectionItem)
                {
                    s1 = "";
                    s2 = System.IO.Path.GetFileName((item as IFileNameItem).FileName);
                }
                else if (item is IFileNameItem)
                {
                    s1 = System.IO.Path.GetDirectoryName((item as IFileNameItem).FileName);
                    s2 = System.IO.Path.GetFileName((item as IFileNameItem).FileName);
                }
                else if (item is FeatureDatasetItem)
                {
                    if (item.Parent is IFileNameItem)
                    {
                        s1 = System.IO.Path.GetDirectoryName((item.Parent as IFileNameItem).FileName);
                        s2 = System.IO.Path.GetFileName((item.Parent as IFileNameItem).FileName);
                        s3 = item.Name;
                    }
                    else
                        return "";
                }
                string s = s1;
                if (s2.Length > 0)
                {
                    s = s + "," + s2;
                    if (s3.Length > 0)
                        s = s + "," + s3;
                }
                return s;
            }
        }

        /// <summary>
        /// ��ָ�����ı��ַ�������������
        /// </summary>
        /// <param name="text">�ı��ַ�</param>
        /// <returns>����������</returns>
        public static DataItem LoadFromString(string text)
        {
            //try
            //{
            string[] str = text.Split(',');
            if (str.Length == 1)
            {
                DataItem fitem = new FolderItem(str[0]);
                CreateParentItems_Folder(fitem);
                return fitem;
            }
            else
            {
                DataItem pitem = GetWorkspaceItem(str[0], str[1]);
                //����һ��DataItem�����������ſ���ʹ����һ������
                if (str[0].Trim().Length == 0) //sde
                {
                    DataItem tmpItem = new DatabaseRootItem();
                    pitem.Parent = tmpItem;
                }
                else
                    CreateParentItems_Folder(pitem);

                if (str.Length > 2)
                {
                    if (str[2].Trim().Length > 0)
                    {
                        DataItem item = GetFeatureDatasetItem(pitem.GetGeoObject() as ESRI.ArcGIS.Geodatabase.IFeatureWorkspace, str[2]);
                        item.Parent = pitem;
                        return item;
                    }
                    else
                        return pitem;
                }
                else
                    return pitem;

            }
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// ��ָ����·�����ı��л�ȡ�����ռ�������
        /// </summary>
        /// <param name="path">·��</param>
        /// <param name="text">�ı��ַ�</param>
        /// <returns>���ع����ռ�</returns>
        private static DataItem GetWorkspaceItem(string path, string text)
        {
            DataItem item;
            string ext = System.IO.Path.GetExtension(text).ToLower();
            if (ext == ".sde")
            {
                string conFolder = CommonConst.m_ConnectPropertyPath;

                item = new DatabaseConnectionItem(conFolder + "\\" + text);
                return item;
            }
            else if (ext == ".gdb")
            {
                item = new FileGdbItem(path + "\\" + text);
                return item;
            }
            else if (ext == ".mdb")
            {
                item = new AccessGdbItem(path + "\\" + text);
                return item;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ��ָ���Ĺ����ռ���ı��л�ȡʸ�����ݼ�
        /// </summary>
        /// <param name="ws">�����ռ�</param>
        /// <param name="text">�ı�</param>
        /// <returns>����ʸ�����ݼ�</returns>
        private static DataItem GetFeatureDatasetItem(ESRI.ArcGIS.Geodatabase.IFeatureWorkspace ws, string text)
        {
            try
            {
                ESRI.ArcGIS.Geodatabase.IFeatureDataset ds = ws.OpenFeatureDataset(text);
                if (ds == null) return null;
                DataItem item = new FeatureDatasetItem(ds.FullName as ESRI.ArcGIS.Geodatabase.IFeatureDatasetName);
                return item;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        /// <param name="item">������</param>
        /// <returns>��������</returns>
        private static string GetName(DataItem item)
        {
            if (item is FeatureDatasetItem)
            {
                return item.Name;
            }
            else if (item is AccessGdbItem)
            {
                return item.Name + ".mdb";
            }
            else if (item is FileGdbItem)
            {
                return item.Name + ".gdb";
            }
            else if (item is DatabaseConnectionItem)
            {
                return item.Name + ".sde";
            }
            return "";
        }

        /// <summary>
        /// �ж��������Ƿ�ֱ�����л�
        /// </summary>
        /// <param name="item">������</param>
        /// <returns>���֧���򷵻�true�����򷵻�false</returns>
        private static bool Supported(DataItem item)
        {
            if (item is FeatureDatasetItem)
            {
                return true;
            }
            else if (item is AccessGdbItem)
            {
                return true;
            }
            else if (item is FileGdbItem)
            {
                return true;
            }
            else if (item is DatabaseConnectionItem)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ������һ������
        /// </summary>
        /// <param name="item">������</param>
        private static void CreateParentItems_Folder(DataItem item)
        {
            if ((item is IFileNameItem) == false) return;

            DataItem lastItem = item;
            string fn = (item as IFileNameItem).FileName;
            string folder = System.IO.Path.GetDirectoryName(fn);
            if (string.IsNullOrEmpty(folder))
            {

            }
            else
            {
                while (folder.Length > 2)
                {
                    DataItem pitem = new FolderItem(folder);
                    lastItem.Parent = pitem;
                    lastItem = pitem;

                    //ԭ���Ĵ��룬����Ѵ��̸�·��������C:/д��ȥ�����ڸ��˾Ϳ�����
                    if (folder.Length <= 3)
                    {
                        break;
                    }

                    folder = System.IO.Directory.GetParent(folder).FullName;
                }
            }
            DataItem ditem = new DiskConnectionRootItem();
            lastItem.Parent = ditem;
        }
    }
}
