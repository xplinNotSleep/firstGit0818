namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// 元数据 数据现状枚举类型
    /// </summary>
    public enum  EnumMetaDataStatusType
    {
        完成,
        历史档案,
        作废,
        连续更新,
        正在建设中
    }

    /// <summary>
    /// 元数据 土地利用分类枚举类型
    /// </summary>
    public enum EnumMetaDataClassifyType
    {
        旧分类,
        过滤分类,
        新分类,
        二次调查分类
    }

    /// <summary>
    /// 元数据 数据使用限制类型
    /// </summary>
    public enum EnumMetaUseRestrictType
    {
        无限制,
        版权,
        专利权,
        正在申请专利权,
        许可证,
        知识产权,
        未规定
    }

    /// <summary>
    /// 元数据 数据安全类型
    /// </summary>
    public enum EnumMetaSafeCodeType
    {
        绝密,
        机密,
        秘密,
        限制,
        内部,
        无限制
    }

    /// <summary>
    /// 元数据 数据表示方式
    /// </summary>
    public enum EnumMetaDisplayType
    {
        矢量,
        栅格,
        文本或表,
        影像,
        矩阵,
        TIN,
        模型,
        剖面,
        其它
    }

    /// <summary>
    /// 元数据 专题内容类别
    /// </summary>
    public enum EnumMetaDataCategoryType
    {
        警用基础数据,          
        警用公共数据,           
        警用业务数据,
        警用法规与文档,
        其它

        //土地产权产籍,
        //土地利用,
        //土地评价,
        //土地市场,
        //土地信访与监察,
        //土地法规与文档,
        //其它
    }

    /// <summary>
    /// 元数据 大地坐标参照系统名称
    /// </summary>
    public enum EnumMetaCoordsRefName
    {
        北京54坐标系,
        西安80坐标系,
        地方独立坐标系,
        WGS84大地坐标系
    }

    /// <summary>
    /// 元数据 坐标系统类型
    /// </summary>
    public enum EnumMetaCoordsType
    {
        笛卡尔坐标系,
        大地坐标系,
        投影坐标系,
        极坐标系,
        重力相关坐标系
    }

    /// <summary>
    /// 元数据表类型
    /// </summary>
    public enum EnumMetaTableType
    {
        标识信息,
        质量信息,
        空间参考信息,
        联系信息,
        分发信息
    }
}
