using System;
using System.Collections.Generic;
using System.Text;
using AG.COM.SDM.Catalog.DataItems;

namespace AG.COM.SDM.Catalog.Filters
{

    /// <summary>
    /// cadҪ���������
    /// </summary>
    public class CADFeatureClassFilter : BaseItemFilter
    {
        public CADFeatureClassFilter()
        {
            m_FilterName = "ʸ��Ҫ�ؼ�";
        }
        #region IDataItemFilter ��Ա

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if (IsWorkspaceOrFeatureDataset(item))
                return true;
            else
                return false;
        }

        public override bool Confirm(DataItem item)
        {
            if ((item.Type == DataType.dtCadPoint) ||
               (item.Type == DataType.dtCadPolyline) ||
               (item.Type == DataType.dtCadPolygon) ||
               (item.Type == DataType.dtCadMultiPatch) ||
               (item.Type == DataType.dtCadAnno))
                return true;
            else
                return false;
        }

        #endregion
    }
    /// <summary>
    /// ʸ��Ҫ���������
    /// </summary>
    public class FeatureClassFilter:BaseItemFilter
    {
        public FeatureClassFilter()
        {
            m_FilterName = "ʸ��Ҫ�ؼ�"; 
        }
        #region IDataItemFilter ��Ա

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if (IsWorkspaceOrFeatureDataset(item))
                return true;
            else
                return false;           
        }

        public override bool Confirm(DataItem item)
        {
            if ((item.Type == DataType.dtFeatureClass) ||
               (item.Type == DataType.dtPointFeatureClass) ||
               (item.Type == DataType.dtLineFeatureClass) ||
               (item.Type == DataType.dtAreaFeatureClass) ||
               (item.Type == DataType.dtFeatureDataset) ||
               (item.Type == DataType.dtShapeFile) ||
               (item.Type == DataType.dtCadDrawing) ||
               (item.Type == DataType.dtCadDrawingDataset) ||
               (item.Type == DataType.dtAnnoFeatureClass)||
               (item.Type == DataType.dtTable))
                return true;
            else
                return false;
        }
          
        #endregion
    }
    /// <summary>
    /// դ��Ŀ¼
    /// </summary>
    public class RasterCatalogFilter : BaseItemFilter
    {
        public RasterCatalogFilter()
        {
            base.m_FilterName = "դ��Ŀ¼";
        }

        public override bool CanPass(DataItem item)
        {
            return (this.Confirm(item) || (((item.Type == DataType.dtAccess) || (item.Type == DataType.dtFileGdb)) || (item.Type == DataType.dtSdeConnection)));
        }

        public override bool Confirm(DataItem item)
        {
            return (item.Type == DataType.dtRasterCatalog);
        }
    }


    /// <summary>
    /// ��״Ҫ���������
    /// </summary>
    public class PointFeatureClassFilter : BaseItemFilter
    {
        public PointFeatureClassFilter()
        {
            m_FilterName = "��״Ҫ�ؼ�";
        }
        #region IDataItemFilter ��Ա

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if (IsWorkspaceOrFeatureDataset(item))
                return true;
            return false;            
        }

        public override bool Confirm(DataItem item)
        {
            if (item.Type == DataType.dtPointFeatureClass)
                return true;
            else
                return false;
        }

        #endregion
    }

    /// <summary>
    /// ��״Ҫ���������
    /// </summary>
    public class LineFeatureClassFilter : BaseItemFilter
    {
        public LineFeatureClassFilter()
        {
            m_FilterName = "��״Ҫ�ؼ�"; 
        }
        #region IDataItemFilter ��Ա

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if (IsWorkspaceOrFeatureDataset(item))
                return true;
            return false;
        }

        public override bool Confirm(DataItem item)
        {
            if (item.Type == DataType.dtLineFeatureClass)
                return true;
            else
                return false;
        }
 

        #endregion
    }

    /// <summary>
    /// ��״Ҫ���������
    /// </summary>
    public class AreaFeatureClassFilter : BaseItemFilter
    {
        public AreaFeatureClassFilter()
        {
            m_FilterName = "��״Ҫ�ؼ�";
        }
        #region IDataItemFilter ��Ա
        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if (IsWorkspaceOrFeatureDataset(item))
                return true;
            return false;
        }

        public override bool Confirm(DataItem item)
        {
            if (item.Type == DataType.dtAreaFeatureClass)
                return true;
            else
                return false;
        }

          
    }

        #endregion
    

    public class AnnoFeatureClassFilter : BaseItemFilter
    {
        public AnnoFeatureClassFilter()
        {
            m_FilterName = "ע�ǲ�";
        }
        #region IDataItemFilter ��Ա

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if (IsWorkspaceOrFeatureDataset(item))
                return true;
            return false;
        }

        public override bool Confirm(DataItem item)
        {
            if (item.Type == DataType.dtAnnoFeatureClass)
                return true;
            else
                return false;
        }
 
        #endregion
    }

    /// <summary>
    /// ���Ա������
    /// </summary>
    public class TableFilter : BaseItemFilter
    {
        public TableFilter()
        {
            m_FilterName = "���Ա�";
        }
        #region IDataItemFilter ��Ա

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if (IsWorkspaceOrFeatureDataset(item))
                return true;
            return false;
        }

        public override bool Confirm(DataItem item)
        {
            if (item.Type == DataType.dtFileGdb || item.Type==DataType.dtAccess)
                return true;
            else
                return false;
        }

        #endregion
    }

    public class ExcelFilter : BaseItemFilter
    {
        public ExcelFilter()
        {
            m_FilterName = "excel�ļ�";
        }
        #region IDataItemFilter ��Ա

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if (IsWorkspaceOrFeatureDataset(item))
                return true;
            return false;
        }

        public override bool Confirm(DataItem item)
        {
            if (item.Type == DataType.dtExcel)
                return true;
            else
                return false;
        }

        #endregion
    }
}

