using System;
using System.Collections.Generic;
using System.Text;
using AG.COM.SDM.Catalog.DataItems;

namespace AG.COM.SDM.Catalog.Filters
{

    /// <summary>
    /// cad要素类过滤器
    /// </summary>
    public class CADFeatureClassFilter : BaseItemFilter
    {
        public CADFeatureClassFilter()
        {
            m_FilterName = "矢量要素集";
        }
        #region IDataItemFilter 成员

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
    /// 矢量要素类过滤器
    /// </summary>
    public class FeatureClassFilter:BaseItemFilter
    {
        public FeatureClassFilter()
        {
            m_FilterName = "矢量要素集"; 
        }
        #region IDataItemFilter 成员

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
    /// 栅格目录
    /// </summary>
    public class RasterCatalogFilter : BaseItemFilter
    {
        public RasterCatalogFilter()
        {
            base.m_FilterName = "栅格目录";
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
    /// 点状要素类过滤器
    /// </summary>
    public class PointFeatureClassFilter : BaseItemFilter
    {
        public PointFeatureClassFilter()
        {
            m_FilterName = "点状要素集";
        }
        #region IDataItemFilter 成员

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
    /// 线状要素类过滤器
    /// </summary>
    public class LineFeatureClassFilter : BaseItemFilter
    {
        public LineFeatureClassFilter()
        {
            m_FilterName = "线状要素集"; 
        }
        #region IDataItemFilter 成员

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
    /// 面状要素类过滤器
    /// </summary>
    public class AreaFeatureClassFilter : BaseItemFilter
    {
        public AreaFeatureClassFilter()
        {
            m_FilterName = "面状要素集";
        }
        #region IDataItemFilter 成员
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
            m_FilterName = "注记层";
        }
        #region IDataItemFilter 成员

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
    /// 属性表过滤器
    /// </summary>
    public class TableFilter : BaseItemFilter
    {
        public TableFilter()
        {
            m_FilterName = "属性表";
        }
        #region IDataItemFilter 成员

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
            m_FilterName = "excel文件";
        }
        #region IDataItemFilter 成员

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

