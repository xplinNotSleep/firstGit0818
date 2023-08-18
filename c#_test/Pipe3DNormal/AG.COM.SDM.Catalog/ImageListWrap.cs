using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using AG.COM.SDM.Catalog.DataItems;

namespace AG.COM.SDM.Catalog
{
	/// <summary>
	/// ImageListWrap 的摘要说明。
	/// </summary>
	public class ImageListWrap : System.ComponentModel.Component
	{
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;

		public ImageListWrap(System.ComponentModel.IContainer container)
		{
			///
			/// Windows.Forms 类撰写设计器支持所必需的
			///
			container.Add(this);
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		public ImageListWrap()
		{
			///
			/// Windows.Forms 类撰写设计器支持所必需的
			///
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region 组件设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageListWrap));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "AddArcGISServer.png");
            this.imageList1.Images.SetKeyName(1, "AddArcIMSServer.png");
            this.imageList1.Images.SetKeyName(2, "AddLink.png");
            this.imageList1.Images.SetKeyName(3, "AddServerObject.png");
            this.imageList1.Images.SetKeyName(4, "AddWMSServer.png");
            this.imageList1.Images.SetKeyName(5, "Anno.png");
            this.imageList1.Images.SetKeyName(6, "AnnotationCoverage.png");
            this.imageList1.Images.SetKeyName(7, "ArcGISServerConn.png");
            this.imageList1.Images.SetKeyName(8, "ArcGISServerUnConn.png");
            this.imageList1.Images.SetKeyName(9, "ArcIMSServerConn.png");
            this.imageList1.Images.SetKeyName(10, "ArcIMSServerUnConn.png");
            this.imageList1.Images.SetKeyName(11, "CADAnno.png");
            this.imageList1.Images.SetKeyName(12, "CADDrawing.png");
            this.imageList1.Images.SetKeyName(13, "CADLayers.png");
            this.imageList1.Images.SetKeyName(14, "CADMultiPatch.png");
            this.imageList1.Images.SetKeyName(15, "CADPoint.png");
            this.imageList1.Images.SetKeyName(16, "CADPolygon.png");
            this.imageList1.Images.SetKeyName(17, "CADPolyline.png");
            this.imageList1.Images.SetKeyName(18, "Copy.bmp");
            this.imageList1.Images.SetKeyName(19, "Coverage.png");
            this.imageList1.Images.SetKeyName(20, "CoverageAnnoFeatclass.png");
            this.imageList1.Images.SetKeyName(21, "CoverageArcFeatClass.png");
            this.imageList1.Images.SetKeyName(22, "CoverageLabelFeatClass.png");
            this.imageList1.Images.SetKeyName(23, "CoverageNodeFeatClass.png");
            this.imageList1.Images.SetKeyName(24, "CoveragePointFeatClass.png");
            this.imageList1.Images.SetKeyName(25, "CoveragePolygonFeatClass.png");
            this.imageList1.Images.SetKeyName(26, "CoverageRegionFeatClass.png");
            this.imageList1.Images.SetKeyName(27, "CoverageRouteFeatClass.png");
            this.imageList1.Images.SetKeyName(28, "CoverageTicFeatClass.png");
            this.imageList1.Images.SetKeyName(29, "Dataset.png");
            this.imageList1.Images.SetKeyName(30, "DBLink.png");
            this.imageList1.Images.SetKeyName(31, "Delete.bmp");
            this.imageList1.Images.SetKeyName(32, "DiskConnect.png");
            this.imageList1.Images.SetKeyName(33, "Error.bmp");
            this.imageList1.Images.SetKeyName(34, "ErrorLayer.bmp");
            this.imageList1.Images.SetKeyName(35, "ErrorShapeFile.bmp");
            this.imageList1.Images.SetKeyName(36, "Excel.bmp");
            this.imageList1.Images.SetKeyName(37, "Feature.png");
            this.imageList1.Images.SetKeyName(38, "FillFeatureClass.png");
            this.imageList1.Images.SetKeyName(39, "FillLayer.png");
            this.imageList1.Images.SetKeyName(40, "Folder1.png");
            this.imageList1.Images.SetKeyName(41, "Folder2.png");
            this.imageList1.Images.SetKeyName(42, "GDBLink1.png");
            this.imageList1.Images.SetKeyName(43, "GDBLink2.png");
            this.imageList1.Images.SetKeyName(44, "GeometryNet.png");
            this.imageList1.Images.SetKeyName(45, "GISServerFolder.png");
            this.imageList1.Images.SetKeyName(46, "GroupLayer.png");
            this.imageList1.Images.SetKeyName(47, "Layer.png");
            this.imageList1.Images.SetKeyName(48, "LineCoverage.png");
            this.imageList1.Images.SetKeyName(49, "LineFeature.png");
            this.imageList1.Images.SetKeyName(50, "LineLayer.png");
            this.imageList1.Images.SetKeyName(51, "LinkToFolder.png");
            this.imageList1.Images.SetKeyName(52, "MapServerPaused.png");
            this.imageList1.Images.SetKeyName(53, "MapServerStarted.png");
            this.imageList1.Images.SetKeyName(54, "MapServerStoped.png");
            this.imageList1.Images.SetKeyName(55, "MXD.png");
            this.imageList1.Images.SetKeyName(56, "Paste.bmp");
            this.imageList1.Images.SetKeyName(57, "PersonGDB.png");
            this.imageList1.Images.SetKeyName(58, "PMF.png");
            this.imageList1.Images.SetKeyName(59, "PointCoverage.png");
            this.imageList1.Images.SetKeyName(60, "PointFeature.png");
            this.imageList1.Images.SetKeyName(61, "PointLayer.png");
            this.imageList1.Images.SetKeyName(62, "PolygonCoverage.png");
            this.imageList1.Images.SetKeyName(63, "Projection.png");
            this.imageList1.Images.SetKeyName(64, "Property.png");
            this.imageList1.Images.SetKeyName(65, "Raster.png");
            this.imageList1.Images.SetKeyName(66, "RasterCatalog.png");
            this.imageList1.Images.SetKeyName(67, "RasterDataset.png");
            this.imageList1.Images.SetKeyName(68, "RasterLayer.png");
            this.imageList1.Images.SetKeyName(69, "Refresh.png");
            this.imageList1.Images.SetKeyName(70, "RegionCoverage.png");
            this.imageList1.Images.SetKeyName(71, "Relation.png");
            this.imageList1.Images.SetKeyName(72, "Root.png");
            this.imageList1.Images.SetKeyName(73, "ShapeFile.png");
            this.imageList1.Images.SetKeyName(74, "ShapeFill.png");
            this.imageList1.Images.SetKeyName(75, "ShapeLine.png");
            this.imageList1.Images.SetKeyName(76, "ShapePoint.png");
            this.imageList1.Images.SetKeyName(77, "SymbolControlCheck.bmp");
            this.imageList1.Images.SetKeyName(78, "SymbolControlLock.bmp");
            this.imageList1.Images.SetKeyName(79, "SymbolControlUnCheck.bmp");
            this.imageList1.Images.SetKeyName(80, "SymbolControlUnLock.bmp");
            this.imageList1.Images.SetKeyName(81, "Table.png");
            this.imageList1.Images.SetKeyName(82, "Text.png");
            this.imageList1.Images.SetKeyName(83, "Tin.png");
            this.imageList1.Images.SetKeyName(84, "ToHigher.bmp");
            this.imageList1.Images.SetKeyName(85, "Topology.png");
            this.imageList1.Images.SetKeyName(86, "Word.bmp");
            this.imageList1.Images.SetKeyName(87, "ims.bmp");
            this.imageList1.Images.SetKeyName(88, "Excel.png");

		}
		#endregion

        /// <summary>
        /// 获取ImageList列表项
        /// </summary>
		public System.Windows.Forms.ImageList ImageList
		{
			get { return this.imageList1; } 
		}

        /// <summary>
        /// 根据数据类型获取图标索引值
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <returns>返回图标索引值</returns>
		public int GetImageIndex(DataType type)
		{
            switch (type)
            {
                case DataType.dtDisk:
                    return imageList1.Images.IndexOfKey("DiskConnect.png");
                case DataType.dtFolder:
                    return imageList1.Images.IndexOfKey("Folder1.png");
                case DataType.dtAccess:
                    return imageList1.Images.IndexOfKey("PersonGDB.png");
                case DataType.dtFileGdb:
                    return imageList1.Images.IndexOfKey("PersonGDB.png");
                case DataType.dtCadDrawing:
                    return imageList1.Images.IndexOfKey("CADLayers.png");
                case DataType.dtCadDrawingDataset:
                    return imageList1.Images.IndexOfKey("CAdDrawing.png");
                case DataType.dtCadAnno:
                    return imageList1.Images.IndexOfKey("CADAnno.png");
                case DataType.dtCadPoint:
                    return imageList1.Images.IndexOfKey("CADPoint.png");
                case DataType.dtCadPolygon:
                    return imageList1.Images.IndexOfKey("CADPolygon.png");
                case DataType.dtCadPolyline:
                    return imageList1.Images.IndexOfKey("CAdPolyline.png");
                case DataType.dtCadMultiPatch:
                    return imageList1.Images.IndexOfKey("CADMultiPatch.png");
                case DataType.dtLayerFile:
                    return imageList1.Images.IndexOfKey("Layer.png");
                case DataType.dtConverage:
                    return imageList1.Images.IndexOfKey("Coverage.png");
                case DataType.dtFeatureDataset:
                    return imageList1.Images.IndexOfKey("Dataset.png");
                case DataType.dtFeatureClass:
                    return 6;
                case DataType.dtPointFeatureClass:
                    return imageList1.Images.IndexOfKey("PointFeature.png");
                case DataType.dtLineFeatureClass:
                    return imageList1.Images.IndexOfKey("LineFeature.png");
                case DataType.dtAreaFeatureClass:
                    return imageList1.Images.IndexOfKey("FillFeatureClass.png");
                case DataType.dtAnnoFeatureClass:
                    return imageList1.Images.IndexOfKey("Anno.png");
                case DataType.dtImageFile:
                    return imageList1.Images.IndexOfKey("Raster.png");
                case DataType.dtNetwork:
                    return 8;
                case DataType.dtRasterCatalog:
                    return imageList1.Images.IndexOfKey("RasterDataset.png");
                case DataType.dtRasterDataset:
                    return imageList1.Images.IndexOfKey("RasterCatalog.png");
                case DataType.dtShapeFile:
                    return imageList1.Images.IndexOfKey("ShapeFile.png");
                case DataType.dtErrorShapeFile:
                    return imageList1.Images.IndexOfKey("ErrorShapeFile.bmp");
                case DataType.dtTable:
                    return imageList1.Images.IndexOfKey("Table.png");
                case DataType.dtExcel:
                    return imageList1.Images.IndexOfKey("Excel.png");
                case DataType.dtTin:
                    return imageList1.Images.IndexOfKey("Tin.png");
                case DataType.dtTopology:
                    return imageList1.Images.IndexOfKey("Topology.png");
                case DataType.dtSdeConnection:
                    return imageList1.Images.IndexOfKey("DBLink.png");
                case DataType.dtImsService:
                    return imageList1.Images.IndexOfKey("ArcIMSServerConn.png");
                case DataType.dtAddDatabaseConnection:
                    return imageList1.Images.IndexOfKey("AddLink.png");
                case DataType.dtAddIms:
                    return imageList1.Images.IndexOfKey("AddLink.png");
                case DataType.dtHisLocation:
                    return imageList1.Images.IndexOfKey("DiskConnect.png");
                case DataType.dtAddAgsConnection:
                    return imageList1.Images.IndexOfKey("AddArcGISServer.png");
                case DataType.dtAgsConnection:
                    return imageList1.Images.IndexOfKey("ArcGISServerConn.png");
                case DataType.dtAgsService:
                    return imageList1.Images.IndexOfKey("ArcIMSServerConn.png");
                case DataType.dtUnknown:
                    return -1;
                default:
                    return -1;
            }			 
		}
	}
}
