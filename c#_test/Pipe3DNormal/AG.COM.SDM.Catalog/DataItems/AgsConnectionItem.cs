using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System.Threading;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Carto;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// ArcGIS Server数据项 的摘要说明。
	/// </summary>
    public class AgsConnectionItem : DataItem, IFileNameItem
	{
		private string m_FileName = "";

        /// <summary>
        /// 初始化ArcGIS Server数据项实例
        /// </summary>
        /// <param name="connFileName">连接文件名称</param>
        public AgsConnectionItem(string connFileName)
		{
            m_FileName = connFileName;
			m_DataType = DataType.dtAgsService;
		}

        /// <summary>
        /// 获取数据项的名称
        /// </summary>
		public override string Name
		{
			get
			{
                return System.IO.Path.GetFileNameWithoutExtension(m_FileName);
			}
		}

        /// <summary>
        /// 获取ArcGIS Server对象
        /// </summary>
        /// <returns></returns>
        public override object GetGeoObject()
        {          
            IAGSServerConnectionFactory2 acf = new AGSServerConnectionFactoryClass();
            IPropertySet pset = acf.ReadConnectionPropertiesFromFile(m_FileName);

            IAGSServerConnection con = acf.Open(pset, 0);
            return con;
        }

        public override IList<DataItem> GetChildren()
        {
            IList<DataItem> items = new List<DataItem>();
            IAGSServerConnection con = GetGeoObject() as IAGSServerConnection;
            if (con == null)
                return items;
            IAGSEnumServerObjectName pEnum = con.ServerObjectNames;
            pEnum.Reset();
            IAGSServerObjectName agsname = pEnum.Next();
            while (agsname != null)
            {
                if (agsname.Type == "MapServer")
                {                                     
                    DataItem item = new AgsServiceItem(agsname);
                    item.Parent = this;
                    items.Add(item);
                }
                agsname = pEnum.Next();
            }

            return items;
        }


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

        #region IFileNameItem 成员

        public string FileName
        {
            get 
            {
                return m_FileName;
            }
        }

        #endregion
    }
}
