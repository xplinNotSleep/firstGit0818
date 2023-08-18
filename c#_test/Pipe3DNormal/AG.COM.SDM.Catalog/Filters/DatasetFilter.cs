namespace AG.COM.SDM.Catalog.Filters
{
    /// <summary>
    /// ʸ��Ҫ�����ݼ�������
    /// </summary>
    public class FeatureDatasetFilter : BaseItemFilter
    {
        /// <summary>
        /// ��ʼ���˶����ʵ������
        /// </summary>
        public FeatureDatasetFilter()
        {
            m_FilterName = "Ҫ�����ݼ�";
        }

        #region IDataItemFilter ��Ա
        /// <summary>
        /// �ж�ָ��������������Ƿ���ܰ���ʸ�����ݼ�����
        /// </summary>
        /// <param name="item">ָ�������������</param>
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
        /// �ж�ָ������������������Ƿ�Ϊʸ�����ݼ�����
        /// </summary>
        /// <param name="item">���������</param>
        /// <returns>������������������Ϊʸ�����ݼ������򷵻�true,���򷵻�false</returns>
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
    /// RasterDataset������
    /// </summary>
    public class RasterDatasetFilter : BaseItemFilter
    {
        public RasterDatasetFilter()
        {
            m_FilterName = "դ�����ݼ�";
        }

        #region IDataItemFilter ��Ա

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
    /// Raster������
    /// </summary>
    public class RasterFilter : BaseItemFilter
    {
        public RasterFilter()
        {
            m_FilterName = "դ������";
        }

        #region IDataItemFilter ��Ա

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
    /// Ӱ���ļ�������
    /// </summary>
    public class ImageFileFilter : BaseItemFilter
    {
        public ImageFileFilter()
        {
            m_FilterName = "Ӱ���ļ�";
        }

        #region IDataItemFilter ��Ա

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
            m_FilterName = "CAD�ļ�";
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
