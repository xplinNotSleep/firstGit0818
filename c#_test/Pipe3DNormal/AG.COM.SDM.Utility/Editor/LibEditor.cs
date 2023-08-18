using System;
using AG.COM.SDM.Utility.Logger;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Utility.Editor
{
    /// <summary>
    /// 编辑辅助类
    /// </summary>
    public class LibEditor
    {
        /// <summary>
        /// 复制属性数据，不复制图形、二进制等数据，属性为null时也不复制
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

                //面积，长度等非编辑字段不复制
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
                                    ExceptionLog.LogError(string.Format("复制字段 {0} 时出错！", fldName), ex);
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取验证后的字段列表
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
        /// 重置指定的空间对象
        /// </summary>
        /// <param name="pGeometry">指定的空间对象</param>
        /// <param name="pRefGeometry">参考对象</param>
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
        /// 重置指定的空间对象
        /// </summary>
        /// <param name="pGeometry">指定的空间对象</param>
        /// <param name="pGeometryDef">空间参考定义</param>
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
        /// 获取新的可编辑的工作空间
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <returns>返回新的可编辑的工作空间</returns>
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
