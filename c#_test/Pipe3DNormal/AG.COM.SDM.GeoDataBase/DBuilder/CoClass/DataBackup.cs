using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 数据备份类
    /// </summary>
    public class DataBackup
    {
        /// <summary>
        /// 获取或设置工作空间
        /// </summary>
        public IWorkspace SourceWorkspace { get; set; }

        /// <summary>
        /// 获取或设置输出路径
        /// </summary>
        public string ExportFolder { get; set; }

        /// <summary>
        /// 获取或设置输出数据集名称
        /// </summary>
        public string ExportDatasetName { get; set; }

        /// <summary>
        /// 获取或设置数据文件名称
        /// </summary>
        public string ExportFileName { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DataBackup()
        {
            
        }

        /// <summary>
        /// 创建MDB工作空间
        /// </summary>
        /// <returns></returns>
        public IWorkspace CreateMDBWorkspace()
        {
            if (!Directory.Exists(ExportFolder))
            {
                MessageBox.Show("输出路径不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            string strFullPath = ExportFolder + ExportFileName + ".mdb";
            IWorkspaceFactory wsf = new AccessWorkspaceFactoryClass();
            IWorkspace workspace = null;
            if (File.Exists(strFullPath))
            {
                File.Delete(strFullPath);
            }
            wsf.Create(ExportFolder, ExportFileName, null, 0);
            workspace = wsf.OpenFromFile(strFullPath, 0);
            return workspace;
        }

        /// <summary>
        /// 创建GDB工作空间
        /// </summary>
        /// <returns></returns>
        public IWorkspace CreateGDBWorkspace()
        {
            string strFullPath = ExportFolder + ExportFileName + ".gdb";
            IWorkspaceFactory wsf = new FileGDBWorkspaceFactoryClass();
            IWorkspace workspace = null;
            if (Directory.Exists(strFullPath))
            {
                string[] strFiles = Directory.GetFiles(strFullPath);
                foreach (string strFileName in strFiles)
                {
                    File.Delete(strFileName);
                }
                Directory.Delete(strFullPath);
            }

            wsf.Create(ExportFolder, ExportFileName, null, 0);

            workspace = wsf.OpenFromFile(strFullPath, 0);
            return workspace;
        }

        public virtual IFeatureDataset CreateFeatureDataset(IFeatureWorkspace sourceWorkspace, IFeatureWorkspace tarWorkspace, string sourceDatasetName)
        {
            if (sourceWorkspace == null)
                return null;
            IFeatureDataset sourceFeatDataset = GetFeatureDataset(sourceWorkspace, sourceDatasetName);
            IGeoDataset geoDataset = sourceFeatDataset as IGeoDataset;
            if (geoDataset == null)
                return null;
            string strDatasetName = RemovePrefix(sourceFeatDataset.Name);
            IFeatureDataset tFeatureDataset = tarWorkspace.CreateFeatureDataset(strDatasetName, geoDataset.SpatialReference);
            return tFeatureDataset;
        }

        public virtual bool Export(IWorkspace pSourceWorkspace,IFeatureWorkspace targetWorkspace,IFeatureDataset tarFeatDataset, string strDatasetName, string strSourceTableName,string strFeatureType)
        {
            IFeatureClass tSourceFeatClass = GetFeatureClass(pSourceWorkspace, strSourceTableName);
            if (tSourceFeatClass == null)
                return false;
            //创建要素类
            IFields tFields = GetCloneFields(tSourceFeatClass.Fields);
            IFeatureClass tOutPutFeatureClass = null;
            string strFeatClsName = RemovePrefix(tSourceFeatClass.AliasName);
            if (tSourceFeatClass.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                IAnnoClass dataExt = tSourceFeatClass.Extension as IAnnoClass;
                IAnnoClass ext = tSourceFeatClass.Extension as IAnnoClass;
                IGraphicsLayerScale layerScale = new GraphicsLayerScaleClass();
                layerScale.ReferenceScale = ext.ReferenceScale;
                layerScale.Units = ext.ReferenceScaleUnits;
                tOutPutFeatureClass = GeoDBHelper.CreateFeatureAnnoClass(targetWorkspace, strFeatClsName, tFields, string.Empty,
                    tarFeatDataset, tSourceFeatClass, dataExt.AnnoProperties, layerScale, dataExt.SymbolCollection, true);
            }
            else
            {
                if (tarFeatDataset == null)
                {
                    tOutPutFeatureClass = GeoDBHelper.CreateFeatureClass(targetWorkspace, strFeatClsName, esriFeatureType.esriFTSimple, tFields, null, null, "");
                }
                else
                {
                    tOutPutFeatureClass = GeoDBHelper.CreateFeatureClass(tarFeatDataset, strFeatClsName, esriFeatureType.esriFTSimple, tFields, null, null, "");
                }
            }
            if (tOutPutFeatureClass == null) return false;

            //获取字段匹配规则 
            Dictionary<int, int> tDictFieldsRule = GetOutPutFieldsRule(tOutPutFeatureClass.Fields, tSourceFeatClass.Fields);
            //导入数据到指定的要素类
            GeoDBHelper.ImportDataToFeatureClass(tSourceFeatClass, tOutPutFeatureClass, tDictFieldsRule, null);
            return true;
        }

        public string RemovePrefix(string strValue)
        {
            string result = strValue;
            int indexDot = strValue.IndexOf('.');
            if (indexDot >= 0 && indexDot != strValue.Length - 1)
            {
                result = strValue.Substring(indexDot + 1);
            }
            return result;
        }

        public IFeatureClass GetFeatureClass(IWorkspace workspace, string strTabelName)
        {
            IWorkspace2 ws2 = workspace as IWorkspace2;
            bool bln = ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, strTabelName);
            if (bln)
            {
                return (workspace as IFeatureWorkspace).OpenFeatureClass(strTabelName);
            }
            else
            {
                return null;
            }
        }

        public IFields GetCloneFields(IFields pFields)
        {
            IClone clone = pFields as IClone;
            return clone.Clone() as IFields;
        }

        /// <summary>
        /// 通过要素数据集名称获取要素数据集
        /// </summary>
        /// <param name="pFeatureWorkspace">基于地理要素的工作空间</param>
        /// <param name="strDatasetName">要素数据集名称</param>
        /// <returns></returns>
        public IFeatureDataset GetFeatureDataset(IFeatureWorkspace pFeatureWorkspace, string strDatasetName)
        {
            IWorkspace2 workspace = pFeatureWorkspace as IWorkspace2;
            if (workspace.get_NameExists(esriDatasetType.esriDTFeatureDataset, strDatasetName))
            {
                return pFeatureWorkspace.OpenFeatureDataset(strDatasetName);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取输出数据字段的对应规则
        /// </summary>
        /// <param name="pOutPutFields">要输出的数据字段</param>
        /// <returns>返回字段匹配规则
        /// <key>表示目标字段索引</key>
        /// <value>表示源字段索引</value>
        /// </returns>
        public Dictionary<int, int> GetOutPutFieldsRule(IFields pOutPutFields, IFields pInputFields)
        {
            if (pOutPutFields == null || pInputFields == null) return null;

            //实例化匹配规则(key为目标要素字段对象索引值, value为源要素字段索引值)
            Dictionary<int, int> tDictOutputRule = new Dictionary<int, int>();

            for (int i = 0; i < pInputFields.FieldCount; i++)
            {
                IField tField = pInputFields.get_Field(i);

                if (tField.Type == esriFieldType.esriFieldTypeOID ||
                    tField.Type == esriFieldType.esriFieldTypeRaster ||
                    tField.Type == esriFieldType.esriFieldTypeGUID ||
                    tField.Type == esriFieldType.esriFieldTypeGlobalID ||
                    tField.Type == esriFieldType.esriFieldTypeGeometry ||
                    tField.Name.ToUpper().Contains("SHAPE") == true)
                {
                    continue;
                }
                else
                {
                    int toIndex = -1;
                    toIndex = pOutPutFields.FindField(tField.Name);

                    if (toIndex > -1)
                    {
                        //添加字段匹配规则
                        tDictOutputRule.Add(toIndex, i);
                    }
                }
            }
            return tDictOutputRule;
        }
    }

}
