using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System.Threading;
using ESRI.ArcGIS.GISClient;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// ArcIMS连接数据项 的摘要说明。
	/// </summary>
    public class ImsConnectionItem : DataItem, IFileNameItem
	{
		private string m_FileName = "";

        /// <summary>
        /// 初始化ArcIMS连接数据项实例对象
        /// </summary>
        /// <param name="connFileName"></param>
        public ImsConnectionItem(string connFileName)
		{
            m_FileName = connFileName;
			m_DataType = DataType.dtImsService;
		}

        /// <summary>
        /// 获取数据项名称
        /// </summary>
		public override string Name
		{
			get
			{
                return System.IO.Path.GetFileNameWithoutExtension(m_FileName);
			}
		}
    
        /// <summary>
        /// 获取数据项对象
        /// </summary>
        /// <returns>如果不为空则返回IIMSMapLayer项，否则返回空</returns>
        public override object GetGeoObject()
        {
            IMemoryBlobStream stream = new MemoryBlobStreamClass();

            ESRI.ArcGIS.Carto.IIMSMapLayer layer = null;
            try
            {              
                stream.LoadFromFile(m_FileName);
                IIMSServiceDescription service = new IMSServiceNameClass();
                (service as IPersistStream).Load(stream);

                layer = new ESRI.ArcGIS.Carto.IMSMapLayerClass();
                layer.ConnectToService(service);
            }         
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(stream);
            }

            return layer;
        }  

        public override bool HasChildren
        {
            get
            {
                return false;
            }
        }

        public override bool CanLoad
        {
            get
            {
                return true;
            }
        }

        #region IFileNameItem 成员

        /// <summary>
        /// 获取文件名称
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }
        }

        #endregion
    }
}
