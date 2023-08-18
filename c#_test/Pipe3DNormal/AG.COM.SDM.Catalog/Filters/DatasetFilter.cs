namespace AG.COM.SDM.Catalog.Filters
{
    /// <summary>
    /// 矢量要素数据集过滤器
    /// </summary>
    public class FeatureDatasetFilter : BaseItemFilter
    {
        /// <summary>
        /// 初始化此对象的实例对象
        /// </summary>
        public FeatureDatasetFilter()
        {
            m_FilterName = "要素数据集";
        }

        #region IDataItemFilter 成员
        /// <summary>
        /// 判断指定的数据项对象是否可能包含矢量数据集类型
        /// </summary>
        /// <param name="item">指定的数据项对象</param>
        /// <returns></returns>
        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if ((item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtAccess) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFileGdb) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtSdeConnection) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFolder) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtDisk))
                return true;
            else
                return false; 
        }

        /// <summary>
        /// 判断指定的数据项对象类型是否为矢量数据集类型
        /// </summary>
        /// <param name="item">数据项对象</param>
        /// <returns>如果数据项项对象类型为矢量数据集类型则返回true,否则返回false</returns>
        public override bool Confirm(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFeatureDataset)
                return true;
            else
                return false; 
        }

        #endregion
    }

    /// <summary>
    /// RasterDataset过滤器
    /// </summary>
    public class RasterDatasetFilter : BaseItemFilter
    {
        public RasterDatasetFilter()
        {
            m_FilterName = "栅格数据集";
        }

        #region IDataItemFilter 成员

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if ((item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtAccess) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFileGdb) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtSdeConnection) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFolder) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtDisk))
                return true;
            else
                return false; 
        }

        public override bool Confirm(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtRasterDataset)
                return true;
            else
                return false;
        }

        #endregion
    }

    /// <summary>
    /// Raster过滤器
    /// </summary>
    public class RasterFilter : BaseItemFilter
    {
        public RasterFilter()
        {
            m_FilterName = "栅格数据";
        }

        #region IDataItemFilter 成员

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if ((item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtAccess) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFileGdb) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtSdeConnection) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFolder) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtDisk))
                return true;
            else
                return false;
        }

        public override bool Confirm(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if ((item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtRasterDataset) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtRasterCatalog) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtImageFile))
                return true;
            else
                return false;
        }
 

        #endregion
    }

    /// <summary>
    /// 影像文件过滤器
    /// </summary>
    public class ImageFileFilter : BaseItemFilter
    {
        public ImageFileFilter()
        {
            m_FilterName = "影像文件";
        }

        #region IDataItemFilter 成员

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtImageFile)
                return true;
            else
                return false;
        }
  
        #endregion
    }

    public class CadFileFilter:BaseItemFilter 
    {
        public CadFileFilter()
        {
            m_FilterName = "CAD文件";
        }

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtCadDrawing)
                return true;
            else
                return false;
        }
    }
}
