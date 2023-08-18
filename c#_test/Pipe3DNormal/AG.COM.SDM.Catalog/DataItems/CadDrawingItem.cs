using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// CAD整图
    /// </summary>
    public class CadDrawingItem:DataItem 
    {
        private string m_FileName = "";

        /// <summary>
        /// 初始化CAD整理实例对象
        /// </summary>
        /// <param name="fileName">文件路径</param>
        public CadDrawingItem(string fileName)
        {
            m_FileName = fileName;
            m_DataType = DataType.dtCadDrawing;
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
        /// 获取其是否还有子节点
        /// </summary>
        public override bool HasChildren
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 获取其是否可以加载到地图或在软件界面中打开的状态值
        /// </summary>
        public override bool CanLoad
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 获取子节点对象
        /// </summary>
        /// <returns>返回CAD数据集下的子节点对象</returns>
        public override IList<DataItem> GetChildren()
        {
            IList<DataItem> items = new List<DataItem>();

            DataItem item = new CadFeatureClassItem(this.m_FileName, DataType.dtCadAnno);
            item.Parent = this;          
            items.Add(item);

            item = new CadFeatureClassItem(this.m_FileName, DataType.dtCadPoint);
            item.Parent = this;
            items.Add(item);

            item = new CadFeatureClassItem(this.m_FileName, DataType.dtCadPolyline);
            item.Parent = this;
            items.Add(item);

            item = new CadFeatureClassItem(this.m_FileName, DataType.dtCadPolygon);
            item.Parent = this;
            items.Add(item);

            item = new CadFeatureClassItem(this.m_FileName, DataType.dtCadMultiPatch);
            item.Parent = this;
            items.Add(item);

            item = new CadDatasetItem(this.m_FileName);
            item.Parent = this;
            items.Add(item);

            return items;
        }
    }
}
