namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// ���Դ���������
    /// </summary>
    public class ItemPropertyFactory
    {
        /// <summary>
        /// ���ݽ�����ʹ�����Ӧ��������
        /// </summary>
        /// <param name="dataNodeItem">�������</param>
        /// <returns>�������Խ��</returns>
        public ItemProperty Create(EnumDataNodeItems dataNodeItem)
        {
            switch (dataNodeItem)
            {
                case EnumDataNodeItems.DataBaseItem:
                    return new DataBaseItemProperty();  
                case EnumDataNodeItems.SubjectChildItem:
                    return new SubjectItemProperty();
                case EnumDataNodeItems.DataSetItem:
                    return new DataSetItemProperty();                    
                case EnumDataNodeItems.FeatureClassItem:
                    return new FeatureClassItemProperty();
                case EnumDataNodeItems.CustomTableItem:
                    return new CustomTableItemProperty();
                case EnumDataNodeItems.CustomFieldItem:
                    return new FieldItemProperty();
                case EnumDataNodeItems.ObjectFieldItem:
                    return new ObjectFieldItemProperty();
                case EnumDataNodeItems.GeometryFieldItem:
                    return new GeometryFieldItemProperty();
                case EnumDataNodeItems.DomainItem:
                    return new DomainItemProperty();
                case EnumDataNodeItems.SubjectDomainItem:
                    return new SubDomainItemProperty();
                //#region ע���ֶ�

                //case EnumDataNodeItems.FeatureID:
                //    return new FeatureIDItemProperty();
                //case EnumDataNodeItems.ZOrder:
                //    return new ZOrderItemProperty();
                //case EnumDataNodeItems.AnnotationClassID:
                //    return new AnnotationClassIDPropery();
                //case EnumDataNodeItems.Element:
                //    return new ElementItemProperty();
                //case EnumDataNodeItems.SymbolID:
                //    return new SymbolIDItemProperty();
                //case EnumDataNodeItems.Status:
                //    return new StatusItemProperty();
                //case EnumDataNodeItems.TextString:
                //    return new TextStringItemProperty();
                //case EnumDataNodeItems.FontName:
                //    return new FontNameItemProperty();
                //case EnumDataNodeItems.FontSize:
                //    return new FontSizeItemProperty();

                //case EnumDataNodeItems.Bold:
                //    return new BoldItemProperty();
                //case EnumDataNodeItems.Italic:
                //    return new ItalicItemProperty();
                //case EnumDataNodeItems.Underline:
                //    return new UnderlineItemProperty();
                //case EnumDataNodeItems.VerticalAlignment:
                //    return new VerticalAlignmentProty();
                //case EnumDataNodeItems.HorizontalAlignment:
                //    return new HorizontalAlignmentProty();
                //case EnumDataNodeItems.XOffset:
                //    return new XOffsetItemProperty();
                //case EnumDataNodeItems.YOffset:
                //    return new YOffsetItemProperty();
                //case EnumDataNodeItems.Angle:
                //    return new AngleItemProperty();
                //case EnumDataNodeItems.FontLeading:
                //    return new FontLeadingProperty();
                //case EnumDataNodeItems.WordSpacing:
                //    return new WordSpacingProperty();
                //case EnumDataNodeItems.CharacterWidth:
                //    return new CharacterWidthProty();
                //case EnumDataNodeItems.CharacterSpacing:
                //    return new CharacterSpacingProty();

                //case EnumDataNodeItems.FlipAngle:
                //    return new FlipAngleItemProperty();
                //case EnumDataNodeItems.Override:
                //    return new OverrideItemProperty();

                //#endregion
                default:
                    return null;
            }
        }
    }
}
