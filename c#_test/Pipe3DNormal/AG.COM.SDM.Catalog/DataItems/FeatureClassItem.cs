using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// 矢量要素类数据项 的摘要说明。
	/// </summary>
	public class FeatureClassItem:DataItem
	{
        private IFeatureClassName m_FeatureClass = null;
        private IWorkspace pWorkspace = null;
        public FeatureClassItem(IFeatureClassName featureclass)
        {
            m_FeatureClass = featureclass;
            if (m_FeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                m_DataType = DataType.dtAnnoFeatureClass;
            }
            else
            {
                if ((m_FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint) ||
                    (m_FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint))
                    m_DataType = DataType.dtPointFeatureClass;
                else if (m_FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    m_DataType = DataType.dtLineFeatureClass;
                else
                    m_DataType = DataType.dtAreaFeatureClass;
            }
        }
        public FeatureClassItem(IFeatureClassName featureclass, IWorkspace ws)
        {
            m_FeatureClass = featureclass;
            if (m_FeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                m_DataType = DataType.dtAnnoFeatureClass;
            }
            else
            {
                if ((m_FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint) ||
                    (m_FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint))
                    m_DataType = DataType.dtPointFeatureClass;
                else if (m_FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    m_DataType = DataType.dtLineFeatureClass;
                else
                    m_DataType = DataType.dtAreaFeatureClass;
            }
        }


        public override string Name
		{
			get
			{
                return (m_FeatureClass as IDatasetName).Name;
			}
		}

        public override string AliasName
        {
            get
            {
                return Name;
            }
        }
        //public override IWorkspace Workspace { get { return pWorkspace; } }
        public override object GetGeoObject()
        {
            return (m_FeatureClass as IName).Open();
        }        
	}
}
