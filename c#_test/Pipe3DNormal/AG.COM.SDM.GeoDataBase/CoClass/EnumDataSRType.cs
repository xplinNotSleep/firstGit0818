namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 空间参考类型
    /// </summary>
    public enum EnumDataSRType
    {
        /// <summary>
        /// 要素集类型
        /// </summary>
        DatasetSR,
        /// <summary>
        /// 要素类类型
        /// </summary>
        FeatureClassSR,
        /// <summary>
        /// Shape文件类型
        /// </summary>
        ShapeSR,
    }

    /// <summary>
    /// 要素类型
    /// </summary>
    public enum EnumFeatureType
    {
        简要要素类 = 1,
        简单连接要素类 = 7,
        简单边要素类 = 8,
        复杂连接要素类 = 9,
        复杂边要素类 = 10,
        注记要素类 = 11
    }

    /// <summary>
    /// 枚举字段类型
    /// </summary>
    public enum EnumFieldItems
    {
        短整型 = 0,
        整型 = 1,
        单精度 = 2,
        双精度 = 3,
        字符型 = 4,
        日期型 = 5,
        OID = 6,
        几何对象 = 7,
        二进制 = 8
    }

    /// <summary>
    /// 枚举几何类型
    /// </summary>
    public enum EnumGeometryItems
    {
        点 = 1,
        多点 = 2,
        线 = 3,
        面 = 4,
        切片 = 9
    }
}
