namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// ö�����ݽ������
    /// </summary>
    public enum  EnumDataNodeItems
    {
        None,
        /// <summary>
        /// ���ݿ���
        /// </summary>
        DataBaseItem, 
        /// <summary>
        /// ���ݼ���
        /// </summary>
        DataSetItem,
        /// <summary>
        /// Ҫ������
        /// </summary>
        FeatureClassItem,
        /// <summary>
        /// ���Ա���
        /// </summary>
        CustomTableItem,
        /// <summary>
        /// ר���ӿ���
        /// </summary>
        SubjectChildItem,
        /// <summary>
        /// �Զ����ֶ���
        /// </summary>
        CustomFieldItem,
        /// <summary>
        /// �����ֶ���
        /// </summary>
        GeometryFieldItem,
        /// <summary>
        /// Object�ֶ���
        /// </summary>
        ObjectFieldItem,
        /// <summary>
        /// ��������
        /// </summary>
        DomainItem ,
        /// <summary>
        /// ������ר�����
        /// </summary>
        SubjectDomainItem,

        #region ע��ר����
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

    /// <summary>
    /// ö���ֶ�����
    /// </summary>
    public enum EnumFieldItems
    {
        ������ = 0,
        ����   = 1,
        ������ = 2,
        ˫���� = 3,
        �ַ��� = 4,
        ������ = 5,
        OID    = 6,
        ���ζ���=7,
        ������ = 8        
    }

    /// <summary>
    /// ������Ϣչʾ״̬
    /// </summary>
    public enum EnumDbInfoStates
    {
        /// <summary>
        /// �½����ݱ�׼
        /// </summary>
        New,
        /// <summary>
        /// �༭���ݱ�׼
        /// </summary>
        Edit,
        /// <summary>
        /// ���
        /// </summary>
        Browse,
        /// <summary>
        /// �������ݿ�
        /// </summary>
        CreateDataBase,
    }

    /// <summary>
    /// Ҫ������
    /// </summary>
    public enum EnumFeatureType
    {
        ��Ҫ����=1,
        ������Ҫ����=7,
        �򵥱�Ҫ����=8,
        ��������Ҫ����=9,
        ���ӱ�Ҫ����=10,
        ע��Ҫ����=11
    }
}
