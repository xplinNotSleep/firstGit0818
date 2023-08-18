namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// �ռ�ο�����
    /// </summary>
    public enum EnumDataSRType
    {
        /// <summary>
        /// Ҫ�ؼ�����
        /// </summary>
        DatasetSR,
        /// <summary>
        /// Ҫ��������
        /// </summary>
        FeatureClassSR,
        /// <summary>
        /// Shape�ļ�����
        /// </summary>
        ShapeSR,
    }

    /// <summary>
    /// Ҫ������
    /// </summary>
    public enum EnumFeatureType
    {
        ��ҪҪ���� = 1,
        ������Ҫ���� = 7,
        �򵥱�Ҫ���� = 8,
        ��������Ҫ���� = 9,
        ���ӱ�Ҫ���� = 10,
        ע��Ҫ���� = 11
    }

    /// <summary>
    /// ö���ֶ�����
    /// </summary>
    public enum EnumFieldItems
    {
        ������ = 0,
        ���� = 1,
        ������ = 2,
        ˫���� = 3,
        �ַ��� = 4,
        ������ = 5,
        OID = 6,
        ���ζ��� = 7,
        ������ = 8
    }

    /// <summary>
    /// ö�ټ�������
    /// </summary>
    public enum EnumGeometryItems
    {
        �� = 1,
        ��� = 2,
        �� = 3,
        �� = 4,
        ��Ƭ = 9
    }
}
