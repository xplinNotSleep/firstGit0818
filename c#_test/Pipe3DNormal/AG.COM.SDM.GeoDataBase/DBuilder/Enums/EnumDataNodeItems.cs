namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 枚举数据结点类型
    /// </summary>
    public enum  EnumDataNodeItems
    {
        None,
        /// <summary>
        /// 数据库项
        /// </summary>
        DataBaseItem, 
        /// <summary>
        /// 数据集项
        /// </summary>
        DataSetItem,
        /// <summary>
        /// 要素类项
        /// </summary>
        FeatureClassItem,
        /// <summary>
        /// 属性表项
        /// </summary>
        CustomTableItem,
        /// <summary>
        /// 专题子库项
        /// </summary>
        SubjectChildItem,
        /// <summary>
        /// 自定义字段项
        /// </summary>
        CustomFieldItem,
        /// <summary>
        /// 几何字段项
        /// </summary>
        GeometryFieldItem,
        /// <summary>
        /// Object字段项
        /// </summary>
        ObjectFieldItem,
        /// <summary>
        /// 属性域项
        /// </summary>
        DomainItem ,
        /// <summary>
        /// 属性域专题库项
        /// </summary>
        SubjectDomainItem,

        #region 注记专用项
        FeatureID,

        ZOrder,

        AnnotationClassID,

        Element,

        SymbolID,

        Status,

        TextString,

        FontName,

        FontSize,

        Bold,

        Italic, 

        Underline,

        VerticalAlignment,

        HorizontalAlignment,

        XOffset,

        YOffset,

        Angle,

        FontLeading,

        WordSpacing,

        CharacterWidth,

        CharacterSpacing,

        FlipAngle,


        Override, 
        #endregion
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

    /// <summary>
    /// 枚举字段类型
    /// </summary>
    public enum EnumFieldItems
    {
        短整型 = 0,
        整型   = 1,
        单精度 = 2,
        双精度 = 3,
        字符型 = 4,
        日期型 = 5,
        OID    = 6,
        几何对象=7,
        二进制 = 8        
    }

    /// <summary>
    /// 数据信息展示状态
    /// </summary>
    public enum EnumDbInfoStates
    {
        /// <summary>
        /// 新建数据标准
        /// </summary>
        New,
        /// <summary>
        /// 编辑数据标准
        /// </summary>
        Edit,
        /// <summary>
        /// 浏览
        /// </summary>
        Browse,
        /// <summary>
        /// 创建数据库
        /// </summary>
        CreateDataBase,
    }

    /// <summary>
    /// 要素类型
    /// </summary>
    public enum EnumFeatureType
    {
        简单要素类=1,
        简单连接要素类=7,
        简单边要素类=8,
        复杂连接要素类=9,
        复杂边要素类=10,
        注记要素类=11
    }
}
