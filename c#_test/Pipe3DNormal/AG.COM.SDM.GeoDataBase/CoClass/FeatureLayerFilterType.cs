using System;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 要素图层过滤类型
    /// </summary>
    [Flags]
    public enum FeatureLayerFilterType
    {
        lyFilterNone = 0,
        lyFilterPoint = 1,
        lyFilterLine = 2,
        lyFilterArea = 4,
        lyFilterAnno = 8
    }
}
