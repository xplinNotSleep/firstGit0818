using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
    public partial class Ctrl3DMarker : CtrlPropertyBase
    {
        private IGraphicsContainer3D _axesGraphicsContainer3D;
        private IGraphicsContainer3D _multiPatchGraphicsContainer3D;
        /// <summary>
		/// 当前符号
		/// </summary>
		private IMarker3DSymbol m_pSymbol = null;
        public Ctrl3DMarker()
        {
            InitializeComponent();
            this.MouseWheel += Ctrl3DMarker_MouseWheel;
            _axesGraphicsContainer3D = ConstructGraphicsLayer3D("Axes");
            _multiPatchGraphicsContainer3D = ConstructGraphicsLayer3D("MultiPatch");
            axSceneControl1.Scene.AddLayer(_axesGraphicsContainer3D as ILayer, true);
            axSceneControl1.Scene.AddLayer(_multiPatchGraphicsContainer3D as ILayer, true);
            DrawAxes(_axesGraphicsContainer3D);
          
        }
        #region 显示三维图形
        private IGraphicsContainer3D ConstructGraphicsLayer3D(string name)
        {
            IGraphicsContainer3D graphicsContainer3D = new GraphicsLayer3DClass();
            ILayer layer = graphicsContainer3D as ILayer;
            layer.Name = name;

            return graphicsContainer3D;
        }
        private void DrawAxes(IGraphicsContainer3D axesGraphicsContainer3D)
        {
            const esriSimple3DLineStyle AxisStyle = esriSimple3DLineStyle.esriS3DLSTube;
            const double AxisWidth = 0.2;

            DrawAxis(axesGraphicsContainer3D,ConstructPoint3D(-50, 0, 0), ConstructPoint3D(50, 0, 0), GetColor(255, 0, 0), AxisStyle, AxisWidth);
            DrawAxis(axesGraphicsContainer3D, ConstructPoint3D(0, -50, 0), ConstructPoint3D(0, 50, 0),GetColor(0, 0, 255), AxisStyle, AxisWidth);
            DrawAxis(axesGraphicsContainer3D, ConstructPoint3D(0, 0, -50),ConstructPoint3D(0, 0, 50),GetColor(0, 255, 0), AxisStyle, AxisWidth);

            //DrawEnd(axesGraphicsContainer3D,ConstructPoint3D(10, 0, 0), ConstructVector3D(0, 10, 0), 90, GetColor(255, 0, 0), 0.2 * AxisWidth);
            //DrawEnd(axesGraphicsContainer3D, ConstructPoint3D(0, 10, 0), ConstructVector3D(10, 0, 0), -90, GetColor(0, 0, 255), 0.2 * AxisWidth);
            //DrawEnd(axesGraphicsContainer3D, ConstructPoint3D(0, 0, 10), null, 0, GetColor(0, 255, 0), 0.2 * AxisWidth);
            //DrawText(axesGraphicsContainer3D, ConstructPoint3D(10 + 1, 0, 0), "X", 1);
            //DrawText(axesGraphicsContainer3D, ConstructPoint3D(0, 10 + 1, 0), "Y", 1);
            //DrawText(axesGraphicsContainer3D, ConstructPoint3D(0, 0, 10 + 1), "Z", 1);

         
            axSceneControl1.SceneGraph.RefreshViewers();
        }
        private  void DrawAxis(IGraphicsContainer3D axesGraphicsContainer3D, IPoint axisFromPoint, IPoint axisToPoint, IColor axisColor, esriSimple3DLineStyle axisStyle, double axisWidth)
        {
            IPointCollection axisPointCollection = new PolylineClass();

            axisPointCollection.AddPoint(axisFromPoint);
            axisPointCollection.AddPoint(axisToPoint);

            MakeZAware(axisPointCollection as IGeometry);

            ISimpleLine3DSymbol simpleLine3DSymbol = new SimpleLine3DSymbolClass();
            simpleLine3DSymbol.Style = axisStyle;
            simpleLine3DSymbol.ResolutionQuality = 1;

            ILineSymbol lineSymbol = simpleLine3DSymbol as ILineSymbol;
            lineSymbol.Color = axisColor;
            lineSymbol.Width = axisWidth;

            ILine3DPlacement line3DPlacement = lineSymbol as ILine3DPlacement;
            line3DPlacement.Units = esriUnits.esriUnknownUnits;

            ILineElement lineElement = new LineElementClass();
            lineElement.Symbol = lineSymbol;

            IElement element = lineElement as IElement;
            element.Geometry = axisPointCollection as IGeometry;

            axesGraphicsContainer3D.AddElement(element);

        }
        private  void DrawEnd(IGraphicsContainer3D endGraphicsContainer3D, IPoint endPoint, IVector3D axisOfRotationVector3D, double degreesOfRotation, IColor endColor, double endRadius)
        {
            IGeometry endGeometry = GetExample2();

            ITransform3D transform3D = endGeometry as ITransform3D;

            IPoint originPoint =ConstructPoint3D(0, 0, 0);

            transform3D.Scale3D(originPoint, endRadius, endRadius, 2 * endRadius);

            if (degreesOfRotation != 0)
            {
                double angleOfRotationInRadians = GetRadians(degreesOfRotation);

                transform3D.RotateVector3D(axisOfRotationVector3D, angleOfRotationInRadians);
            }

            transform3D.Move3D(endPoint.X - originPoint.X, endPoint.Y - originPoint.Y, endPoint.Z - originPoint.Z);


            DrawMultiPatch(endGraphicsContainer3D, endGeometry);


        }
        private IGeometry GetExample2()
        {
            const double ConeBaseDegrees = 360.0;
            const int ConeBaseDivisions = 36;
            const double VectorComponentOffset = 0.0000001;
            const double ConeBaseRadius = 6;
            const double ConeBaseZ = 0.0;
            const double ConeApexZ = 9.5;

            //Vector3D: Cone, TriangleFan With 36 Vertices

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleFanPointCollection = new TriangleFanClass();

            //Set Cone Apex To (0, 0, ConeApexZ)

            IPoint coneApexPoint = ConstructPoint3D(0, 0, ConeApexZ);

            //Add Cone Apex To Triangle Fan

            triangleFanPointCollection.AddPoint(coneApexPoint);

            //Define Upper Portion Of Axis Around Which Vector Should Be Rotated To Generate Cone Base Vertices

            IVector3D upperAxisVector3D = ConstructVector3D(0, 0, 10);

            //Define Lower Portion of Axis Around Which Vector Should Be Rotated To Generate Cone Base Vertices

            IVector3D lowerAxisVector3D = ConstructVector3D(0, 0, -10);

            //Add A Slight Offset To X or Y Component Of One Of Axis Vectors So Cross Product Does Not Return A Zero-Length Vector

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            //Obtain Cross Product Of Upper And Lower Axis Vectors To Obtain Normal Vector To Axis Of Rotation To Generate Cone Base Vertices

            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;

            //Set Normal Vector Magnitude Equal To Radius Of Cone Base

            normalVector3D.Magnitude = ConeBaseRadius;

            //Obtain Angle Of Rotation In Radians As Function Of Number Of Divisions Within 360 Degree Sweep Of Cone Base

            double rotationAngleInRadians = GetRadians(ConeBaseDegrees / ConeBaseDivisions);

            for (int i = 0; i < ConeBaseDivisions; i++)
            {
                //Rotate Normal Vector Specified Rotation Angle In Radians Around Either Upper Or Lower Axis

                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);

                //Construct Cone Base Vertex Whose XY Coordinates Are The Sum Of Apex XY Coordinates And Normal Vector XY Components

                IPoint vertexPoint = ConstructPoint3D(coneApexPoint.X + normalVector3D.XComponent,
                                                                      coneApexPoint.Y + normalVector3D.YComponent,
                                                                      ConeBaseZ);

                //Add Vertex To TriangleFan

                triangleFanPointCollection.AddPoint(vertexPoint);
            }

            //Re-Add The Second Point Of The Triangle Fan (First Vertex Added) To Close The Fan

            triangleFanPointCollection.AddPoint(triangleFanPointCollection.get_Point(1));

            //Add TriangleFan To MultiPatch

            multiPatchGeometryCollection.AddGeometry(triangleFanPointCollection as IGeometry);

            return multiPatchGeometryCollection as IGeometry;
        }
        private IVector3D ConstructVector3D(double xComponent, double yComponent, double zComponent)
        {
            IVector3D vector3D = new Vector3DClass();
            vector3D.SetComponents(xComponent, yComponent, zComponent);

            return vector3D;
        }
        private void DrawMultiPatch(IGraphicsContainer3D multiPatchGraphicsContainer3D, IGeometry geometry)
        {
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = 255;
            rgbColor.Green = 255;
            rgbColor.Blue = 255;

            IColor color = rgbColor as IColor;
            color.Transparency = (byte)255;

            multiPatchGraphicsContainer3D.DeleteAllElements();


            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
            simpleFillSymbol.Color = color;

            IElement element = new MultiPatchElementClass();
            element.Geometry = geometry;

            IFillShapeElement fillShapeElement = element as IFillShapeElement;
            fillShapeElement.Symbol = simpleFillSymbol;

            multiPatchGraphicsContainer3D.AddElement(element);

          
        }
        private IPoint ConstructPoint3D(double x, double y, double z)
        {
            IPoint point = ConstructPoint2D(x, y);
            point.Z = z;

            MakeZAware(point as IGeometry);

            return point;
        }
        private double GetRadians(double decimalDegrees)
        {
            return decimalDegrees * (Math.PI / 180);
        }
        private void MakeZAware(IGeometry geometry)
        {
            IZAware zAware = geometry as IZAware;
            zAware.ZAware = true;
        }
        private IPoint ConstructPoint2D(double x, double y)
        {
            IPoint point = new PointClass();
            point.X = x;
            point.Y = y;

            return point;
        }
        private IColor GetColor(int red, int green, int blue)
        {
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = red;
            rgbColor.Green = green;
            rgbColor.Blue = blue;

            IColor color = rgbColor as IColor;
            color.Transparency = (byte)255;

            return color;
        }
        #endregion

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 46) && (e.KeyChar !=8))
            {
                e.Handled = true;
            }
          
        }

        private void tbDX_ValueChanged(object sender, EventArgs e)
        {
            txtDX.Text = (tbDX.Value * 0.01).ToString();
            
        }

        private void tbDY_ValueChanged(object sender, EventArgs e)
        {
            txtDY.Text = (tbDY.Value * 0.01).ToString();
        }

        private void tbDZ_ValueChanged(object sender, EventArgs e)
        {
            txtDZ.Text = (tbDZ.Value * 0.01).ToString();
        }
        private int TxtChanged(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt)) txt = "0";
            double dV = double.Parse(txt);
            if (dV > 1) dV = 1;
            if (dV < 0) dV = 0;
            return (int)(dV * 100);
        }

        private void txtDX_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                tbDX.Value = TxtChanged(txtDX.Text);
            }
        }

        private void txtDY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbDY.Value = TxtChanged(txtDY.Text);
            }
        }

        private void txtDZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbDZ.Value = TxtChanged(txtDZ.Text);
            }
        }

        private void Ctrl3DMarker_Load(object sender, EventArgs e)
        {
            _axesGraphicsContainer3D = ConstructGraphicsLayer3D("Axes");
            _multiPatchGraphicsContainer3D = ConstructGraphicsLayer3D("MultiPatch");
            axSceneControl1.Scene.AddLayer(_axesGraphicsContainer3D as ILayer, true);
            axSceneControl1.Scene.AddLayer(_multiPatchGraphicsContainer3D as ILayer, true);
            DrawAxes(_axesGraphicsContainer3D);
            ControlsSceneNavigateToolClass sTool = new ControlsSceneNavigateToolClass();
            axSceneControl1.CurrentTool = sTool;
            Zoom(-0.3);
            m_pSymbol = m_pCtrlSymbol as IMarker3DSymbol;
            // 根据符号属性设置当前窗体控件值
            if (null != m_pSymbol)
            {
                IMarker3DPlacement pMarker3DPlacement = m_pSymbol as IMarker3DPlacement;
                if (pMarker3DPlacement.Shape == null) return;
                numDepth.Value = (decimal)pMarker3DPlacement.Depth;
                numWidth.Value = (decimal)pMarker3DPlacement.Width;
                numSize.Value = (decimal)pMarker3DPlacement.Size;
                cbAspectRatio.Checked = pMarker3DPlacement.MaintainAspectRatio;

                numOffsetX.Value = (decimal)pMarker3DPlacement.XOffset;

                tbDX.Value = (int)(pMarker3DPlacement.NormalizedOriginOffset.XComponent * 100);
                tbDY.Value = (int)(pMarker3DPlacement.NormalizedOriginOffset.YComponent * 100);
                tbDZ.Value = (int)(pMarker3DPlacement.NormalizedOriginOffset.ZComponent * 100);
                IPoint CenterPnt = ConstructPoint3D(0, 0, 0);
                IGeometry geometry = null;
                pMarker3DPlacement.ApplyToPoint(CenterPnt, out geometry);
                DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
                axSceneControl1.SceneGraph.RefreshViewers();

                axSceneControl1.SceneViewer.Camera.Observer = CenterPnt;
                axSceneControl1.SceneViewer.Camera.Zoom(0.3);
            }
        }
        private void Ctrl3DMarker_MouseWheel(object sender, MouseEventArgs e)
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
        private void Zoom(double scale)
        {
            ICamera pCamera = axSceneControl1.Camera;
            IPoint pPtObs = pCamera.Observer;
            IPoint pPtTar = pCamera.Target;
            pPtObs.X += (pPtObs.X - pPtTar.X) * scale;
            pPtObs.Y += (pPtObs.Y - pPtTar.Y) * scale;
            pPtObs.Z += (pPtObs.Z - pPtTar.Z) * scale;
            pCamera.Observer = pPtObs;

            axSceneControl1.SceneGraph.RefreshViewers();
        }
        private void btnSelPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "模型文件(*.dae)|*.dae";
            openFileDlg.Title = "选择模型文件";
            openFileDlg.InitialDirectory = Application.StartupPath + "\\附属物";
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                lblPath.Text = openFileDlg.FileName;
                LoadModel(openFileDlg.FileName);
            }
        }

        private void LoadModel(string file)
        {
            m_pSymbol.CreateFromFile(file);
            IMarker3DPlacement pMarker3DPlacement = m_pSymbol as IMarker3DPlacement;
            numDepth.Value = (decimal)pMarker3DPlacement.Depth;
            numWidth.Value = (decimal)pMarker3DPlacement.Width;
            numSize.Value = (decimal)pMarker3DPlacement.Size;
            cbAspectRatio.Checked = pMarker3DPlacement.MaintainAspectRatio;

            numOffsetX.Value = (decimal)pMarker3DPlacement.XOffset;

            tbDX.Value = (int)(pMarker3DPlacement.NormalizedOriginOffset.XComponent * 100);
            tbDY.Value = (int)(pMarker3DPlacement.NormalizedOriginOffset.YComponent * 100);
            tbDZ.Value = (int)(pMarker3DPlacement.NormalizedOriginOffset.ZComponent * 100);
            IPoint CenterPnt = ConstructPoint3D(0, 0, 0);
            IGeometry geometry = null;
            pMarker3DPlacement.ApplyToPoint(CenterPnt, out geometry);
            DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            axSceneControl1.SceneGraph.RefreshViewers();
            this.m_pSymbolLayer.UpdateLayerView(m_pSymbol, this.m_iLayerIndex);
            Zoom(-0.5);
            //pictureBox1.Image = StyleCommon.SymbolToBitmp(m_pSymbol as ISymbol, pictureBox1.Width, pictureBox1.Height);

        }
     
        private void numWidth_ValueChanged(object sender, EventArgs e)
        {
            IMarker3DPlacement pMarker3DPlacement = m_pSymbol as IMarker3DPlacement;
            pMarker3DPlacement.Width = (double)numWidth.Value;
            IPoint CenterPnt = ConstructPoint3D(0, 0, 0);
            IGeometry geometry = null;
            pMarker3DPlacement.ApplyToPoint(CenterPnt, out geometry);
            DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            axSceneControl1.SceneGraph.RefreshViewers();
        }

        private void numDepth_ValueChanged(object sender, EventArgs e)
        {
            IMarker3DPlacement pMarker3DPlacement = m_pSymbol as IMarker3DPlacement;
            pMarker3DPlacement.Depth = (double)numDepth.Value;
            IPoint CenterPnt = ConstructPoint3D(0, 0, 0);
            IGeometry geometry = null;
            pMarker3DPlacement.ApplyToPoint(CenterPnt, out geometry);
            DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            axSceneControl1.SceneGraph.RefreshViewers();
        }

        private void numSize_ValueChanged(object sender, EventArgs e)
        {
            IMarker3DPlacement pMarker3DPlacement = m_pSymbol as IMarker3DPlacement;
            pMarker3DPlacement.Size = (double)numSize.Value;
            IPoint CenterPnt = ConstructPoint3D(0, 0, 0);
            IGeometry geometry = null;
            pMarker3DPlacement.ApplyToPoint(CenterPnt, out geometry);
            DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            axSceneControl1.SceneGraph.RefreshViewers();
        }

        private void colorPickerForeground_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}
