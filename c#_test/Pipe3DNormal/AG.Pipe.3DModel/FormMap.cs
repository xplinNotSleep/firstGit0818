using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using AG.COM.SDM.Utility;

namespace AG.Pipe.Analyst3DModel
{
    public partial class FormMap : SkinForm
    {
        public FormMap()
        {
            InitializeComponent();
        }

        private string m_ErrFile = "";

        public string ErrFile
        {
            get { return m_ErrFile; }
            set { m_ErrFile = value; }
        }
        public IList<InputRecord> m_listErr;

        
        /// <summary>
        /// 从列表当中清除错误记录集
        /// </summary>
        public void ClearListItems()
        {
            // this.listErrRecord.Groups.Clear();
            // this.listErrRecord.Items.Clear();
        }
        /// <summary>
        /// 绑定错误信息
        /// </summary>
        /// <param name="listErr">错误记录信息</param>
        public void BindListErr(IList<InputRecord> listErr)
        {
            if (listErr.Count == 0) return;
            this.m_listErr = listErr;
            foreach (InputRecord curErr in listErr)
            {
                //现用\n拆开每5个为一组
                string[] details = curErr.Detail.Split('\n');

                foreach (string detail in details)
                {
                    try
                    {
                        string[] messages = detail.Split(',');
                        string layerName = messages[0];
                        string remark = messages[1];

                        this.dataGridView1.RowCount++;

                        DataGridViewRow newRow = this.dataGridView1.Rows[this.dataGridView1.RowCount - 1];
                        newRow.Cells["TCol_LayerName"].Value = layerName;
                        newRow.Cells["TCol_Detail"].Value = remark;
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 加载导出的gdb 文件
        /// </summary>
        /// <param name="filepath"></param>
        public void LoadGdb(string filepath)
        {
            this.axMapControl1.ClearLayers();

            #region
            //加载影像地图
            //string RasterLayerName = FieldConfigHelper.GetLayerName("Raster");
            //if (m_hookHelper != null)
            //{
            //    IEnumLayer tEnumLayer = m_hookHelper.FocusMap.get_Layers(null, true);

            //    for (ILayer tLayer = tEnumLayer.Next(); tLayer != null; tLayer = tEnumLayer.Next())
            //    {
            //        string layername = tLayer.Name;
            //        if(layername.Contains('.'))
            //        {
            //            string[] names = layername.Split('.');
            //            layername = names[names.Length - 1];
            //        }
            //        if (layername == RasterLayerName)
            //        {
            //            this.axMapControl1.AddLayer(tLayer);
            //            break;
            //        }
            //    }
            //}
            #endregion

            IWorkspace workspace = OpenWorkSpace(filepath);
            List<IFeatureClass> featureClasses = GetFeatureClasses(workspace);
            ILayer currentLayer = null;
            foreach (IFeatureClass item in featureClasses)
            {
                IFeatureLayer featureLayer = new FeatureLayerClass();
                featureLayer.FeatureClass = item;
                this.axMapControl1.AddLayer(featureLayer as ILayer);
                currentLayer = featureLayer;
            }
            if(currentLayer !=null)
            {
                //axMapControl1.ActiveView.Extent = ((IGeoDataset)currentLayer).Extent;//设置地图显示视图大小为当前加载图层的视图大小
                axMapControl1.ActiveView.FocusMap.MapScale = 2000;
                //axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, currentLayer, axMapControl1.ActiveView.Extent);
                axMapControl1.ActiveView.Refresh();
                int layercout = this.axMapControl1.LayerCount;
            }
          
          

        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private IWorkspace OpenWorkSpace(string fileName)
        {
            IWorkspaceFactory factory = new AccessWorkspaceFactoryClass();
            string extention = Path.GetExtension(fileName).ToLower();
            if (extention == ".gdb")
            {
                factory = new FileGDBWorkspaceFactoryClass();
            }
            try
            {
                return factory.OpenFromFile(fileName, 0);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private List<IFeatureClass> GetFeatureClasses(IWorkspace pWorkspace)
        {
            if (pWorkspace == null) throw new ArgumentNullException(nameof(pWorkspace));
            List<IFeatureClass> sFeatureClasses = new List<IFeatureClass>();
            IEnumDataset enumDataset = pWorkspace.Datasets[esriDatasetType.esriDTFeatureClass];
            IDataset dataset = enumDataset.Next();
            while (dataset != null)
            {
                sFeatureClasses.Add((IFeatureClass)dataset);
                dataset = enumDataset.Next();
            }
            enumDataset = pWorkspace.Datasets[esriDatasetType.esriDTFeatureDataset];
            dataset = enumDataset.Next();
            while (dataset != null)
            {
                if (dataset.Subsets != null)
                {
                    IEnumDataset enumDataset1 = dataset.Subsets;
                    IDataset dataset1 = enumDataset1.Next();

                    while (dataset1 != null)
                    {
                        IFeatureClass featureClass = (IFeatureClass)dataset1;
                        sFeatureClasses.Add((IFeatureClass)dataset1);

                        dataset1 = enumDataset1.Next();
                    }
                }
                dataset = enumDataset.Next();
            }


            return sFeatureClasses;
        }


        public void ExportErrRecord()
        {
            try
            {
                if (this.m_ErrFile == string.Empty)
                {
                    SaveFileDialog tSaveFileDialog = new SaveFileDialog();
                    tSaveFileDialog.DefaultExt = "csv";
                    tSaveFileDialog.Filter = "转换日志文件(*.csv)|*.csv";
                    tSaveFileDialog.Title = "转换日志文件";
                    if (tSaveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //this.m_ErrFile = tSaveFileDialog.FileName;
                        //导出转换日志文件
                        this.ExportErrRecord(tSaveFileDialog.FileName);
                    }
                }
                else
                {
                    //导出转换日志文件
                    this.ExportErrRecord(this.m_ErrFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存错误记录文件过程中出错.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 导出转换日志文件
        /// </summary>
        /// <param name="filename">文件路径</param>
        private void ExportErrRecord(string filename)
        {
            using (StreamWriter tStreamWriter = new StreamWriter(filename, false, Encoding.GetEncoding("gb2312")))
            {
                tStreamWriter.Write("图层名称,描述,管线或管点编号,起点信息,终点信息\n");

                foreach (InputRecord inputRecord in this.m_listErr)
                {
                    tStreamWriter.Write(inputRecord.Detail);
                }
            }

            if (MessageBox.Show("保存成功,是否查看打开的文档？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(filename);
            };
        }

    }
}
