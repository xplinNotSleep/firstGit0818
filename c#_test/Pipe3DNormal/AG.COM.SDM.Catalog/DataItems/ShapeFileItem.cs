using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// Shape文件数据项 的摘要说明。
    /// </summary>
    public class ShapeFileItem : DataItem
    {
        private string m_FileName = "";

        /// <summary>
        /// 初始化Shape文件数据项实例对象
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public ShapeFileItem(string fileName)
        {
            m_FileName = fileName;
            string s1 = System.IO.Path.GetDirectoryName(fileName);
            string s2 = System.IO.Path.GetFileNameWithoutExtension(fileName);
            string fn1 = s1 + "\\" + s2 + ".shx";
            string fn2 = s1 + "\\" + s2 + ".dbf";
            if (System.IO.File.Exists(fn1) && System.IO.File.Exists(fn2))
            {
                m_DataType = DataType.dtShapeFile;

                System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open);
                //System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                //br.ReadBytes(24);
                //int FileLength = br.ReadInt32();//<0代表数据长度未知
                //int FileBanben = br.ReadInt32();
                //int ShapeType = br.ReadInt32();

                fs.Seek(32, System.IO.SeekOrigin.Begin);
                int ShapeType = fs.ReadByte();
                fs.Close();

                switch (ShapeType)
                {
                    case 1:
                        m_DataType = DataType.dtPointFeatureClass;
                        break;
                    case 3:
                        m_DataType = DataType.dtLineFeatureClass;
                        break;
                    case 5:
                        m_DataType = DataType.dtAreaFeatureClass;
                        break;
                    case 8:  //注记
                        m_DataType = DataType.dtPointFeatureClass;
                        break;
                    default:
                        break;
                }
            }
            else
                m_DataType = DataType.dtErrorShapeFile;
        }

        /// <summary>
        /// 获取数据项的名称
        /// </summary>
        public override string Name
        {
            get
            {
                return System.IO.Path.GetFileName(m_FileName);
            }
        }

        /// <summary>
        /// 获取shape文件图层对象
        /// </summary>
        /// <returns>返回shape文件图层对象</returns>
        public override object GetGeoObject()
        {
            if (m_DataType == DataType.dtErrorShapeFile)
                return null;
            else
            {
                IPropertySet pPropertySet = new PropertySetClass();
                pPropertySet.SetProperty("Database", System.IO.Path.GetDirectoryName(m_FileName));
                IWorkspaceFactory wsf = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactory();
                IWorkspace ws = wsf.Open(pPropertySet, 0);
                return (ws as IFeatureWorkspace).OpenFeatureClass(System.IO.Path.GetFileName(m_FileName));
            }
        }
    }
}
