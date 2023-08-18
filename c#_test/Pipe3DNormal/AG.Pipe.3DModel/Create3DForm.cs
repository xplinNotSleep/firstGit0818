using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.StylePropertyEdit;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AG.Pipe.Analyst3DModel
{
    public partial class Create3DForm : SkinForm
    {
        public Create3DForm()
        {
            InitializeComponent();
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.axSceneControl1_Wheel);
        }
        private void axSceneControl1_Wheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (axSceneControl1.Visible == true)
            {
                try
                {
                    if (axSceneControl1.Camera.ProjectionType == esri3DProjectionType.esriOrthoProjection)
                    {
                        System.Drawing.Point pSceLoc = axSceneControl1.PointToScreen(this.axSceneControl1.Location);
                        System.Drawing.Point Pt = this.PointToScreen(e.Location);
                        if (Pt.X < pSceLoc.X | Pt.X > pSceLoc.X + axSceneControl1.Width | Pt.Y < pSceLoc.Y | Pt.Y > pSceLoc.Y + axSceneControl1.Height) return;
                        double scale = 0.6;
                        if (e.Delta > 0) scale = 1.4;
                        IEnvelope enve = axSceneControl1.Camera.OrthoViewingExtent;
                        enve.Expand(scale, scale, true);
                        ICamera3 pCamera = axSceneControl1.Camera as ICamera3;
                        pCamera.OrthoViewingExtent_2 = enve;
                        axSceneControl1.SceneGraph.RefreshViewers();
                    }
                    else
                    {
                        System.Drawing.Point pSceLoc = axSceneControl1.PointToScreen(this.axSceneControl1.Location);
                        System.Drawing.Point Pt = this.PointToScreen(e.Location);
                        if (Pt.X < pSceLoc.X | Pt.X > pSceLoc.X + axSceneControl1.Width | Pt.Y < pSceLoc.Y | Pt.Y > pSceLoc.Y + axSceneControl1.Height) return;
                        double scale = 0.2;
                        //if (e.Delta < 0) scale = -0.2;
                        if (e.Delta > 0) scale = -0.2;
                        ICamera pCamera = axSceneControl1.Camera;
                        IPoint pPtObs = pCamera.Observer;
                        IPoint pPtTar = pCamera.Target;
                        pPtObs.X += (pPtObs.X - pPtTar.X) * scale;
                        pPtObs.Y += (pPtObs.Y - pPtTar.Y) * scale;
                        pPtObs.Z += (pPtObs.Z - pPtTar.Z) * scale;
                        pCamera.Observer = pPtObs;

                        axSceneControl1.SceneGraph.RefreshViewers();


                    }

                }
                catch (Exception ex)
                {
                }
            }
        }
        ILayer m_Layer;
        private void axTOCControl_OnDoubleClick(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            esriTOCControlItem toccItem = esriTOCControlItem.esriTOCControlItemNone;
            ILayer iLayer = null;
            IBasicMap iBasicMap = null;
            object unk = null;
            object data = null;

            if (e.button == 1)
            {
                axTOCControl1.HitTest(e.x, e.y, ref toccItem, ref iBasicMap, ref iLayer, ref unk, ref data);
                //如果用户多次点击的是鼠标左键，且点击内容为图层符号，则弹出esri封装好的符号选择窗体
                if (toccItem == esriTOCControlItem.esriTOCControlItemLegendClass)
                {
                    ESRI.ArcGIS.Carto.ILegendClass pLC = new LegendClass();
                    ESRI.ArcGIS.Carto.ILegendGroup pLG = new LegendGroup();
                    if (unk is ILegendGroup)
                    {
                        pLG = (ILegendGroup)unk;
                    }
                    pLC = pLG.get_Class((int)data);
                    ISymbol pSym = pLC.Symbol;
                    ESRI.ArcGIS.DisplayUI.ISymbolSelector pSS = new ESRI.ArcGIS.DisplayUI.SymbolSelectorClass();
                    bool bOK = false;
                    pSS.AddSymbol(pSym);
                    bOK = pSS.SelectSymbol(0);
                    if (bOK)
                    {
                        //pLC.Symbol = pSS.GetSymbolAt(0);
                        //axSceneControl1.SceneGraph.Invalidate(iLayer as IGeoFeatureLayer, true, true);

                        //简单填充符号
                        ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
                        //可以用符号选择器进行
                        if ((pSS.GetSymbolAt(0) as ISimpleFillSymbol) != null)
                        {
                            simpleFillSymbol.Style = (pSS.GetSymbolAt(0) as ISimpleFillSymbol).Style;// esriSimpleFillStyle.esriSFSDiagonalCross;
                            simpleFillSymbol.Color = (pSS.GetSymbolAt(0) as ISimpleFillSymbol).Color;
                        }


                        ISimpleRenderer simpleRender = new SimpleRendererClass();
                        simpleRender.Symbol = simpleFillSymbol as ISymbol;

                        IGeoFeatureLayer tGeoLayer = iLayer as IGeoFeatureLayer;
                        tGeoLayer.Renderer = simpleRender as IFeatureRenderer;
                        axSceneControl1.SceneGraph.Invalidate(iLayer as IGeoFeatureLayer, true, true);
                    }
                    axSceneControl1.SceneGraph.RefreshViewers();
                    axTOCControl1.Update();
                }
            }
        }
        int m_Data;
        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            esriTOCControlItem toccItem = esriTOCControlItem.esriTOCControlItemNone;
            ILayer iLayer = null;
            IBasicMap iBasicMap = null;
            object unk = null;
            object data = null;

            if (e.button == 2)
            {
                axTOCControl1.HitTest(e.x, e.y, ref toccItem, ref iBasicMap, ref iLayer, ref unk, ref data);
                m_Layer = iLayer;
                //m_Data = (int)data;
                if (toccItem == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    contextMenuStrip1.Show(axTOCControl1, new System.Drawing.Point(e.x, e.y));
                    //显示右键菜单，并定义其相对控件的位置，正好在鼠标出显示
                }
                if (toccItem == esriTOCControlItem.esriTOCControlItemMap)
                {
                    contextMenuStrip2.Show(axTOCControl1, new System.Drawing.Point(e.x, e.y));
                    //显示右键菜单，并定义其相对控件的位置，正好在鼠标出显示
                }
            }
        }
        private void 移除图层_Click(object sender, EventArgs e)
        {
            this.axSceneControl1.Scene.DeleteLayer(m_Layer);
            if (dicLayers.ContainsKey(m_Layer.Name))
            {
                dicLayers.Remove(m_Layer.Name);
                Layers.Remove(m_Layer);
            }
            this.axSceneControl1.SceneGraph.RefreshViewers();
            this.axTOCControl1.Update();
        }
        private void 全部移除_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.axSceneControl1.Scene.LayerCount; i++)
            {
                Layers.Remove(this.axSceneControl1.Scene.Layer[i]);
                if (dicLayers.ContainsKey(this.axSceneControl1.Scene.Layer[i].Name))
                {
                    dicLayers.Remove(this.axSceneControl1.Scene.Layer[i].Name);
                }
            }

            this.axSceneControl1.Scene.ClearLayers();
            this.axSceneControl1.SceneGraph.RefreshViewers();
            this.axTOCControl1.Update();
        }
        private void 添加数据_Click(object sender, EventArgs e)
        {
            AG.COM.SDM.Catalog.IDataBrowser tDataBrowser = new AG.COM.SDM.Catalog.FormDataBrowser();
            if (tDataBrowser.ShowDialog() == DialogResult.OK)
            {
                IList<DataItem> tListDataItem = tDataBrowser.SelectedItems;
                IList<ILayer> layers = new List<ILayer>();
                for (int i = 0; i < tListDataItem.Count; i++)
                {
                    object obj = tListDataItem[i].GetGeoObject();
                    //根据指定对象获取图层对象
                    if (obj is IFeatureClass)
                    {
                        IFeatureLayer layer;
                        if ((obj as IFeatureClass).FeatureType == esriFeatureType.esriFTAnnotation)
                            layer = new FDOGraphicsLayerClass();
                        else
                            layer = new FeatureLayerClass();

                        layer.FeatureClass = obj as IFeatureClass;
                        layer.Name = layer.FeatureClass.AliasName;
                        
                        if (layer != null)
                        {
                            #region 在地质岩土3D模型导入时能符号化显示
                            //string geoName = ReadDzSchemes();
                            //if(!string.IsNullOrWhiteSpace(geoName))
                            //{
                            //    int geoNameField = layer.FeatureClass.FindField(geoName);
                            //    if(geoNameField>=0)
                            //    {
                            //        Dictionary<string, IColor> dicGeoNameColor = AnalystHelper.GetColorByGeoName();
                            //        IUniqueValueRenderer pUniqueValueRenderer = AnalystHelper.ZkytModelSymbol
                            //            (layer, dicGeoNameColor, geoName);

                            //        IGeoFeatureLayer tGeoLayer = layer as IGeoFeatureLayer;
                            //        if (pUniqueValueRenderer != null)
                            //        {
                            //            tGeoLayer.Renderer = (IFeatureRenderer)pUniqueValueRenderer;
                            //        }
                            //    }
                            //}
                            #endregion

                            #region
                            //IRgbColor tColor = new RgbColorClass();
                            //tColor.Blue = 0;
                            //tColor.Green = 255;
                            //tColor.Red = 0;
                            ////简单填充符号
                            //ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
                            //simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
                            //simpleFillSymbol.Color = tColor;
                            //ISimpleRenderer simpleRender = new SimpleRendererClass();
                            //simpleRender.Symbol = simpleFillSymbol as ISymbol;
                            //IGeoFeatureLayer tGeoLayer = layer as IGeoFeatureLayer;
                            //tGeoLayer.Renderer = simpleRender as IFeatureRenderer;
                            #endregion

                            this.axSceneControl1.Scene.AddLayer(layer);
                        }

                    }


                }
                this.axSceneControl1.SceneGraph.RefreshViewers();
                this.axTOCControl1.Update();
            }
        }
        private void 更改颜色_Click(object sender, EventArgs e)
        {
            IRgbColor tColor = new RgbColorClass();
            tColor.Blue = 0;
            tColor.Green = 255;
            tColor.Red = 0;
            ////简单填充符号
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
            simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSNull;
            simpleFillSymbol.Color = tColor;

            IGeoFeatureLayer tGeoLayer = m_Layer as IGeoFeatureLayer;
            ISimpleRenderer simpleRender = new SimpleRendererClass();
            simpleRender.Symbol = simpleFillSymbol as ISymbol;
            tGeoLayer.Renderer = simpleRender as IFeatureRenderer;

            //ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
            //ISymbol pSym = simpleFillSymbol as ISymbol;
            //ESRI.ArcGIS.DisplayUI.ISymbolSelector pSS = new ESRI.ArcGIS.DisplayUI.SymbolSelectorClass();
            //bool bOK = false;
            //pSS.AddSymbol(simpleRender.SymbolByFeature[0]);
            //bOK = pSS.SelectSymbol(0);
            //if (bOK)
            //{
            //    ISimpleRenderer simpleRender = new SimpleRendererClass();
            //    simpleRender.Symbol = pSS.GetSymbolAt(0);
            //    tGeoLayer.Renderer = simpleRender as IFeatureRenderer;

            //}
            axSceneControl1.SceneGraph.Invalidate(tGeoLayer, true, true);
            axSceneControl1.SceneGraph.RefreshViewers();
            axTOCControl1.Update();
            axSceneControl1.Refresh();
        }
        List<ILayer> Layers = new List<ILayer>();
        Dictionary<string, ILayer> dicLayers = new Dictionary<string, ILayer>();

        /// <summary>
        /// 生成管线模型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 生成模型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPipe createLineForm = new FormPipe();
            DialogResult dr = createLineForm.ShowDialog();
            if (dr != DialogResult.Yes) return;

            //之前是否存在三维数据
            if (this.axSceneControl1.Scene.LayerCount > 0)
            {
                for (int i = 0; i < this.axSceneControl1.Scene.LayerCount; i++)
                {
                    Layers.Add(this.axSceneControl1.Scene.Layer[i]);//添加之前显示的图层
                    //如果是地图上之前未显示的图层名称
                    if (!dicLayers.ContainsKey(this.axSceneControl1.Scene.Layer[i].Name))
                    {
                        dicLayers.Add(this.axSceneControl1.Scene.Layer[i].Name, this.axSceneControl1.Scene.Layer[i]);
                    }
                    //若是地图上之前已显示过的图层名称
                    else
                    {
                        ILayer rmLayer = this.axSceneControl1.Scene.Layer[i];
                        dicLayers.Remove(this.axSceneControl1.Scene.Layer[i].Name);
                        dicLayers.Add(this.axSceneControl1.Scene.Layer[i].Name, this.axSceneControl1.Scene.Layer[i]);
                        //this.axSceneControl1.Scene.DeleteLayer(rmLayer);//地图上删除掉已显示过的名称
                    }
                }
                this.axSceneControl1.Scene.ClearLayers();//清空原来显示的三维数据
                this.axSceneControl1.SceneGraph.RefreshViewers();
                axTOCControl1.Update();
            }

            IWorkspace pWorkspace = createLineForm.pWorkspace;
            if (pWorkspace == null) return;
            //遍历生成模型的管线方案
            foreach (LineScheme item1 in createLineForm.LineLayer3D)
            {
                foreach (string item in item1.Layer3D)
                {
                    IFeatureClass featureClass = (pWorkspace as IFeatureWorkspace).OpenFeatureClass(item);
                    IFeatureLayer pLayer = new FeatureLayerClass();
                    pLayer.FeatureClass = featureClass;
                    pLayer.Name = featureClass.AliasName;

                    #region 根据方案中的颜色配置填充符号
                    //ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
                    ////可以用符号选择器进行
                    //simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSNull;
                    //simpleFillSymbol.Color = GetRgbColor(item1.LineColor);

                    //ISimpleRenderer simpleRender = new SimpleRendererClass();
                    //simpleRender.Symbol = simpleFillSymbol as ISymbol;

                    //IGeoFeatureLayer tGeoLayer = pLayer as IGeoFeatureLayer;
                    //tGeoLayer.Renderer = simpleRender as IFeatureRenderer;
                    #endregion

                    //在储存之前已显示的图层中查找是否存在相同名称的图层对象
                    if (!dicLayers.ContainsKey(pLayer.Name))
                    {
                        dicLayers.Add(pLayer.Name, pLayer);
                        Layers.Add(pLayer);
                    }
                    else
                    {
                        ILayer hLayer = dicLayers[pLayer.Name];
                        Layers.Remove(hLayer);
                        dicLayers.Remove(pLayer.Name);
                        dicLayers.Add(pLayer.Name, pLayer);
                        Layers.Add(pLayer);
                    }
                }
            }

            //遍历生成模型的管点名称
            foreach (PointScheme item1 in createLineForm.PointLayer3D)
            {
                foreach (string item in item1.Layer3D)
                {
                    IFeatureClass featureClass = (pWorkspace as IFeatureWorkspace).OpenFeatureClass(item);
                    IFeatureLayer pLayer = new FeatureLayerClass();
                    pLayer.FeatureClass = featureClass;
                    pLayer.Name = featureClass.AliasName;

                    
                    if (!dicLayers.ContainsKey(pLayer.Name))
                    {
                        dicLayers.Add(pLayer.Name, pLayer);
                        Layers.Add(pLayer);
                    }
                    else
                    {
                        ILayer hLayer = dicLayers[pLayer.Name];
                        Layers.Remove(hLayer);
                        dicLayers.Remove(pLayer.Name);
                        dicLayers.Add(pLayer.Name, pLayer);
                        Layers.Add(pLayer);
                    }
                }
            }

            //读取存储在字典中的图层的空间参考信息，如果是球面坐标系的，则展示在视图中需要进行投影
            foreach(ILayer layer in dicLayers.Values)
            {
                if(layer is IFeatureLayer)
                {
                    IFeatureLayer fLayer = layer as IFeatureLayer;
                    IFeatureClass fc = fLayer.FeatureClass;
                    IGeoDataset gd = fc as IGeoDataset;
                    if(gd.SpatialReference is IGeographicCoordinateSystem)
                    {
                        ISpatialReference sp= AnalystHelper.GCStoPRJ(gd.SpatialReference);
                        if(sp!=null)
                        {
                            this.axSceneControl1.Scene.SpatialReference = sp;
                        }

                    }
                }
            }

            //先把要显示的三维图层存在字典中，再将其一一显示到屏幕上
            foreach (ILayer pLayer in dicLayers.Values)
            {
                this.axSceneControl1.Scene.AddLayer(pLayer);
            }
            this.axSceneControl1.SceneGraph.RefreshViewers();
            axTOCControl1.Update();

        }

        private void 符号管理器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string StyleFiles = string.Empty;
            DirectoryInfo dirInfo = new DirectoryInfo(CommonConstString.STR_StylePath);
            if (dirInfo.Exists)
            {
                FileInfo[] fileInfos = dirInfo.GetFiles("*.ServerStyle");
                StringBuilder sb = new StringBuilder();
                foreach (FileInfo fileInfo in fileInfos)
                {
                    sb.Append(fileInfo.FullName + "|");
                }

                StyleFiles = sb.ToString();
            }
            string[] styleFiels = StyleFiles.Split('|', ',');
            FormStyleManager frm = new FormStyleManager(styleFiels);
            frm.ShowDialog();
        }

    }
}
