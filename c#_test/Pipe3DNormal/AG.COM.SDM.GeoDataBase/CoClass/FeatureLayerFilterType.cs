using System;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// Ҫ��ͼ���������
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
