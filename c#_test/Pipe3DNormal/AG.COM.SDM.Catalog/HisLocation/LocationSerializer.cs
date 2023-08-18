using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// 空间数据位置序列化
    /// </summary>
    public class LocationSerializer
    {
        /// <summary>
        /// 保存指定的数据项为字符串
        /// </summary>
        /// <param name="item">数据项</param>
        /// <returns>返回字符串</returns>
        public static string SaveToString(DataItem item)
        {
            //保存格式
            //文件夹,工作空间文件,FeatureDataset名
            //如果是sde连接，则文件夹为空
            //工作空间文件可以是.sde或.mdb或.gdb后缀（.gdb后缀的是文件夹，但没有影响）；
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
        /// 从指定的文本字符中载入数据项
        /// </summary>
        /// <param name="text">文本字符</param>
        /// <returns>返回数据项</returns>
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
                //创建一个DataItem的链表，这样才可以使用上一级功能
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
        /// 从指定的路径或文本中获取工作空间数据项
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">文本字符</param>
        /// <returns>返回工作空间</returns>
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
        /// 从指定的工作空间或文本中获取矢量数据集
        /// </summary>
        /// <param name="ws">工作空间</param>
        /// <param name="text">文本</param>
        /// <returns>返回矢量数据集</returns>
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
        /// 获取数据项名称
        /// </summary>
        /// <param name="item">数据项</param>
        /// <returns>返回名称</returns>
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
        /// 判断数据项是否直接序列化
        /// </summary>
        /// <param name="item">数据项</param>
        /// <returns>如果支持则返回true，否则返回false</returns>
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
        /// 创建上一级对象
        /// </summary>
        /// <param name="item">数据项</param>
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

                    //原来的代码，不会把磁盘根路径，例如C:/写进去，现在改了就可以了
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
