using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase.DataBackup
{
    /// <summary>
    /// 数据恢复帮助类
    /// </summary>
    public class DataRecoveryHelper
    {
        /// <summary>
        /// 获取数据集中所有要素类的数据项（DBDataItem）
        /// </summary>
        /// <param name="tFeatureDataset"></param>
        /// <returns></returns>
        public static List<DBDataItem> GetFcDBDataItemInDataset(IFeatureDataset tFeatureDataset)
        {
            List<DBDataItem> result = new List<DBDataItem>();

            if (tFeatureDataset != null)
            {
                IFeatureClassContainer tFeatureClassContainer = tFeatureDataset as IFeatureClassContainer;
                IEnumFeatureClass tEnumFeatureClass = tFeatureClassContainer.Classes;
                tEnumFeatureClass.Reset();
                IFeatureClass tFeatureClass = tEnumFeatureClass.Next();
                while (tFeatureClass != null)
                {
                    IDataset tDataset = tFeatureClass as IDataset;

                    DBDataItem tDBDataItem = new DBDataItem();
                    tDBDataItem.Name = tDataset.Name;
                    tDBDataItem.Type = DataType.FeatureClass;
                    result.Add(tDBDataItem);

                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClass);

                    tFeatureClass = tEnumFeatureClass.Next();
                }

                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumFeatureClass);
            }

            return result;
        }

        /// <summary>
        /// 删除数据集里面的要素类
        /// </summary>
        /// <param name="tFeatureDataset"></param>
        public static void DeleteFeatureClassInDataset(IFeatureDataset tFeatureDataset)
        {
            if (tFeatureDataset != null)
            {
                List<IDataset> tDatasetsDel = new List<IDataset>();
                //由于遍历时删除会出错，因此把要素类全部取出再删除
                IFeatureClassContainer tFeatureClassContainer = tFeatureDataset as IFeatureClassContainer;
                IEnumFeatureClass tEnumFeatureClass = tFeatureClassContainer.Classes;
                tEnumFeatureClass.Reset();
                IFeatureClass tFeatureClass = tEnumFeatureClass.Next();
                while (tFeatureClass != null)
                {
                    IDataset tDataset = tFeatureClass as IDataset;
                    tDatasetsDel.Add(tDataset);

                    tFeatureClass = tEnumFeatureClass.Next();
                }

                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumFeatureClass);

                foreach (IDataset tDataset in tDatasetsDel)
                {
                    //当有锁时不能删除，在此要解锁
                    ReleaseSchemaLock(tDataset);
                  
                    tDataset.Delete();
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tDataset);
                }
            }
        }

        /// <summary>
        /// 删除要素类
        /// </summary>
        /// <param name="tFeatureClass"></param>
        public static void DeleteFeatureClass(IFeatureClass tFeatureClass)
        {
            if (tFeatureClass != null)
            {
                //当有锁时不能删除，在此要解锁
                IDataset tDataset = tFeatureClass as IDataset;
                ReleaseSchemaLock(tDataset);

                tDataset.Delete();
            }
        }

        /// <summary>
        /// 解除SDE锁
        /// </summary>
        /// <param name="tDataset"></param>
        private static void ReleaseSchemaLock(IDataset tDataset)
        {
            //先检查是否有锁，当有锁时用清空sde.table_locks记录来解锁
            ISchemaLock tSchemaLock = tDataset as ISchemaLock;
            IEnumSchemaLockInfo tEnumSchemaLockInfo = null;
            tSchemaLock.GetCurrentSchemaLocks(out tEnumSchemaLockInfo);
            if (tEnumSchemaLockInfo.Next() != null)
            {
                tDataset.Workspace.ExecuteSQL("delete from sde.table_locks");
            }
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumSchemaLockInfo);
        }

        /// <summary>
        /// 创建要素类
        /// </summary>
        /// <param name="tFeatureClassSource"></param>
        /// <param name="tFeatureClassTarget"></param>
        /// <param name="tFeatureWorkspaceAnnoTarget"></param>
        /// <param name="tFeatureDatasetTarget"></param>
        /// <param name="tFeatureWorkspaceTarget"></param>
        /// <param name="tFieldChecker"></param>
        /// <param name="tDBDataItems"></param>
        public static void CreateFeatureClasssReal(IFeatureClass tFeatureClassSource, IFeatureClass tFeatureClassTarget, IFeatureWorkspaceAnno tFeatureWorkspaceAnnoTarget,
           IFeatureDataset tFeatureDatasetTarget, IFeatureWorkspace tFeatureWorkspaceTarget, IFieldChecker tFieldChecker,
          ref List<DBDataItem> tDBDataItems)
        {
            IDataset tDatasetFcSource = tFeatureClassSource as IDataset;

            IClone tClone =tFeatureClassSource.Fields as IClone;
            IFields tFieldsClone = tClone.Clone() as IFields;

            //字段名称存在不兼容的可能，因此要验证字段
            IFields tFieldsValid = null;
            IEnumFieldError tEnumFieldError = null;
            tFieldChecker.Validate(tFieldsClone, out tEnumFieldError, out tFieldsValid);

            //标注要素类与一般要素类建立有不同
            if (tFeatureClassSource.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                IAnnoClass tAnnoClass = tFeatureClassSource.Extension as IAnnoClass;

                IGraphicsLayerScale tGraphicsLayerScale = new GraphicsLayerScaleClass();
                tGraphicsLayerScale.Units = tAnnoClass.ReferenceScaleUnits;
                tGraphicsLayerScale.ReferenceScale = tAnnoClass.ReferenceScale;

                tFeatureClassTarget = tFeatureWorkspaceAnnoTarget.CreateAnnotationClass(tDatasetFcSource.Name, tFieldsValid,
                    tFeatureClassSource.CLSID, tFeatureClassSource.EXTCLSID, tFeatureClassSource.ShapeFieldName,
                    "", tFeatureDatasetTarget, null, tAnnoClass.AnnoProperties, tGraphicsLayerScale,
                    tAnnoClass.SymbolCollection, false);
            }
            else
            {
                //在数据库根目录和数据集创建要素类需要的参数不同
                if (tFeatureDatasetTarget != null)
                {
                    tFeatureClassTarget = tFeatureDatasetTarget.CreateFeatureClass(tDatasetFcSource.Name, tFieldsValid, tFeatureClassSource.CLSID,
                         tFeatureClassSource.EXTCLSID, tFeatureClassSource.FeatureType, tFeatureClassSource.ShapeFieldName, "");
                }
                else
                {
                    tFeatureClassTarget = tFeatureWorkspaceTarget.CreateFeatureClass(tDatasetFcSource.Name, tFieldsValid, tFeatureClassSource.CLSID,
                   tFeatureClassSource.EXTCLSID, tFeatureClassSource.FeatureType, tFeatureClassSource.ShapeFieldName, "");
                }
            }

            DBDataItem tDBDataItemFc = new DBDataItem();
            tDBDataItemFc.Name = (tFeatureClassTarget as IDataset).Name;
            tDBDataItemFc.Type = DataType.FeatureClass;
            tDBDataItems.Add(tDBDataItemFc);
        }

        /// <summary>
        /// 复制要素类
        /// </summary>
        /// <param name="tFeatureClassSource"></param>
        /// <param name="tFeatureClassTarget"></param>
        /// <param name="tWorkspaceEditTarget"></param>
        public static void CopyFeatureClassData(IFeatureClass tFeatureClassSource, IFeatureClass tFeatureClassTarget,
            IWorkspaceEdit tWorkspaceEditTarget, ITrackProgress tTrackProgress)
        {
            IFeatureCursor tFeatureCursorSource = null, tFeatureCursorTarget = null;
            IFeatureBuffer tFeatureBufferTarget = null;
            IFeature tFeatureSource = null;
            //由于两个要素类的字段顺序不一定相同，因此要匹配字段顺序
            Dictionary<int, int> tMatchField = MatchFields(tFeatureClassSource, tFeatureClassTarget);

            tWorkspaceEditTarget.StartEditing(false);
            tWorkspaceEditTarget.StartEditOperation();

            try
            {              
                //源要素类的数据复制到目标要素类
                tFeatureCursorSource = tFeatureClassSource.Search(null, false);
                tFeatureCursorTarget = tFeatureClassTarget.Insert(true);

                tTrackProgress.SubValue = 0;
                tTrackProgress.SubMax = tFeatureClassSource.FeatureCount(null);

                IFields tFieldsTarget = tFeatureClassTarget.Fields;

                tFeatureSource = tFeatureCursorSource.NextFeature();
                while (tFeatureSource != null)
                {
                    tTrackProgress.SubValue++;
                    tTrackProgress.SubMessage = "正在复制第" + tTrackProgress.SubValue.ToString()
                        + "条记录（共" + tTrackProgress.SubMax + "条）";
                    Application.DoEvents();

                    tFeatureBufferTarget = tFeatureClassTarget.CreateFeatureBuffer();

                    tFeatureBufferTarget.Shape = tFeatureSource.ShapeCopy;
                   
                    foreach (KeyValuePair<int, int> kvp in tMatchField)
                    {
                        IField tFieldTarget = tFieldsTarget.get_Field(kvp.Key);
                        //几何字段另外赋值，不可编辑字段不复制
                        if (tFieldTarget.Type != esriFieldType.esriFieldTypeGeometry && tFieldTarget.Editable == true &&
                            kvp.Value >= 0)
                        {
                            tFeatureBufferTarget.set_Value(kvp.Key, tFeatureSource.get_Value(kvp.Value));
                        }
                    }
                    tFeatureCursorTarget.InsertFeature(tFeatureBufferTarget);
                    tFeatureCursorTarget.Flush();                 

                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureBufferTarget);
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureSource);

                    HandleStop(tTrackProgress);

                    tFeatureSource = tFeatureCursorSource.NextFeature();
                }

                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(true);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);

                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(false);

                throw ex;
            }
            finally
            {
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursorSource);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursorTarget);
            }
        }

        /// <summary>
        /// 进度条停止操作判断并处理
        /// </summary>
        private static void HandleStop(ITrackProgress tTrackProgress)
        {
            if (tTrackProgress.IsContinue == false)
            {
                throw new Exception("停止操作");
            }
        }

        /// <summary>
        /// 配对源要素类和目标要素类的字段集
        /// </summary>
        /// <param name="tFeatureClassSource"></param>
        /// <param name="tFeatureClassTarget"></param>
        /// <returns></returns>
        private static Dictionary<int, int> MatchFields(IFeatureClass tFeatureClassSource, IFeatureClass tFeatureClassTarget)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            IFields tFieldsSource = tFeatureClassSource.Fields;
            IFields tFieldsTarget = tFeatureClassTarget.Fields;
            for (int i = 0; i < tFieldsTarget.FieldCount; i++)
            {
                IField tFieldTarget = tFieldsTarget.get_Field(i);
                //字典第一个代表目标字段的index，第二个代表源字段的index，如果找不到源字段则为-1
                result.Add(i, tFieldsSource.FindField(tFieldTarget.Name));
            }

            return result;
        }
    }
}
