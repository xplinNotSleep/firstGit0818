using System;
using AG.COM.SDM.Utility.Logger;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Utility.Editor
{
    /// <summary>
    /// �༭������
    /// </summary>
    public class LibEditor
    {
        /// <summary>
        /// �����������ݣ�������ͼ�Ρ������Ƶ����ݣ�����ΪnullʱҲ������
        /// </summary>
        /// <param name="srcRow"></param>
        /// <param name="destRow"></param>
        public static void CopyFields(IRow srcRow,IRow destRow)
        {
            string fldName;
            int fldIndex;
            IField destField;
            for (int i = 0; i <= destRow.Fields.FieldCount - 1; i++)
            {
                destField = destRow.Fields.get_Field(i);
                fldName = destField.Name;

                //��������ȵȷǱ༭�ֶβ�����
                if (string.Compare(fldName, "SHAPE_Length", true) == 0 || string.Compare(fldName, "SHAPE_Area", true) == 0) continue;

                if ((destField.Type == esriFieldType.esriFieldTypeDate) ||
                    (destField.Type == esriFieldType.esriFieldTypeDouble) ||
                    (destField.Type == esriFieldType.esriFieldTypeInteger) ||
                    (destField.Type == esriFieldType.esriFieldTypeSingle) ||
                    (destField.Type == esriFieldType.esriFieldTypeSmallInteger) ||
                    (destField.Type == esriFieldType.esriFieldTypeString) ||
                    (destField.Type == esriFieldType.esriFieldTypeXML))
                {
                    fldIndex = srcRow.Fields.FindField(fldName);
                    if (fldIndex > 0)
                    {
                        if (srcRow.Fields.get_Field(fldIndex).Type == destField.Type)
                        {
                            object obj = srcRow.get_Value(fldIndex);
                            if (((obj is DBNull) == false) && (obj != null))
                            {
                                try
                                {
                                    destRow.set_Value(i, obj);
                                }
                                catch (Exception ex)
                                {
                                    ExceptionLog.LogError(string.Format("�����ֶ� {0} ʱ����", fldName), ex);
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ��ȡ��֤����ֶ��б�
        /// </summary>
        /// <param name="pFields"></param>
        /// <returns></returns>
        public static IFields GetValidFields(IFields pFields)
        {
            IEnumFieldError pEnumFieldError;
            IFieldChecker pFieldCheck = new FieldCheckerClass();
            IFields pOutFields;
            pFieldCheck.Validate(pFields, out pEnumFieldError, out pOutFields);
            return pOutFields;
        }

        /// <summary>
        /// ����ָ���Ŀռ����
        /// </summary>
        /// <param name="pGeometry">ָ���Ŀռ����</param>
        /// <param name="pRefGeometry">�ο�����</param>
        public static void ResetGeometryMZ(IGeometry pGeometry, IGeometry pRefGeometry)
        {
            if ((pRefGeometry as IZAware).ZAware)
            {
                (pGeometry as IZAware).ZAware = true;

                IZ tIZ = pGeometry as IZ;
                if (tIZ != null)
                    tIZ.SetConstantZ(0);
            }

            if ((pRefGeometry as IMAware).MAware)
            {
                (pGeometry as IMAware).MAware = true;
            }
        }

        /// <summary>
        /// ����ָ���Ŀռ����
        /// </summary>
        /// <param name="pGeometry">ָ���Ŀռ����</param>
        /// <param name="pGeometryDef">�ռ�ο�����</param>
        public static void ResetGeometryMZ(IGeometry pGeometry, IGeometryDef pGeometryDef)
        {
            if (pGeometryDef.HasZ)
            {
                (pGeometry as IZAware).ZAware = true;
                IZ tIZ = pGeometry as IZ;
                if (tIZ != null)
                    tIZ.SetConstantZ(0);
                else
                {
                    IPoint tPoint = pGeometry as IPoint;
                    if (tPoint != null)
                        tPoint.Z = 0;
                }
            }

            if (pGeometryDef.HasM)
            {
                (pGeometry as IMAware).MAware = true;
            }
        }

        /// <summary>
        /// ��ȡ�µĿɱ༭�Ĺ����ռ�
        /// </summary>
        /// <param name="workspace">�����ռ�</param>
        /// <returns>�����µĿɱ༭�Ĺ����ռ�</returns>
        public static IWorkspaceEdit GetNewEditableWorkspace(IWorkspace workspace)
        {
            if ((workspace as IWorkspaceEdit).IsBeingEdited())
            {
                IWorkspace ws = workspace.WorkspaceFactory.Open(workspace.ConnectionProperties, 0);
                return ws as IWorkspaceEdit;
            }
            else
                return workspace as IWorkspaceEdit;
        }
    }
}
