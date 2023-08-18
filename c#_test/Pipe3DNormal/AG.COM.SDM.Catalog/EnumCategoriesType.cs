using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.Catalog
{
    /// <summary>
    /// 数据加载窗体左侧数据源菜单类别
    /// </summary>
    public enum EnumCategoriesType
    {
        /// <summary>
        /// 全部展示
        /// </summary>
        Both = 0,
        /// <summary>
        /// 只展示磁盘目录
        /// </summary>
        Disk = 1,
        /// <summary>
        /// 只展示数据库目录
        /// </summary>
        Database = 2,
        /// <summary>
        /// 只展示服务目录
        /// </summary>
        Service = 3,
        /// <summary>
        /// 展示磁盘和数据库目录
        /// </summary>
        DiskAndDatabase = 4,
        /// <summary>
        /// 展示磁盘和服务目录
        /// </summary>
        DiskAndService = 5,
        /// <summary>
        /// 展示数据库和服务目录
        /// </summary>
        DatabaseAndService = 6,
    }
}
