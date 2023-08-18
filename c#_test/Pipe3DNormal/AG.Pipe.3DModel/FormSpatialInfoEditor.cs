using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
namespace AG.Pipe.Analyst3DModel
{
    public partial class FormSpatialInfoEditor : Form
    {
        private List<PointInfo> m_ListPoints = null;
        private TreeNode m_CopyNode = null;
        private TreeNode m_SelectNode = null;
        private IFields m_Fields = null;
        private ConvertLayerSet m_ConvertLayerSet = null;
        public List<PointInfo> ListPointInfo
        {
            get { return this.m_ListPoints; }

        }
        public FormSpatialInfoEditor(List<PointInfo> pStrPoints,IFields pFields)
        {
            InitializeComponent();
            m_ListPoints = pStrPoints;
            m_Fields = pFields;
            InitPointsInfo();

        }
        public FormSpatialInfoEditor(ConvertLayerSet pConvertLayerSet)
        {
            InitializeComponent();
            m_ListPoints = pConvertLayerSet.Points;
            m_Fields = pConvertLayerSet.PointSource.Fields;
            m_ConvertLayerSet = pConvertLayerSet;
            InitPointsInfo();

        }
        private void InitPointsInfo()
        {
            if (m_ListPoints != null)
            {
                this.treeView1.Nodes.Clear();
                for (int i = 0; i < m_ListPoints.Count; i++)
                {
                    TreeNode tNode = new TreeNode();
                    tNode.Text = "点" + i.ToString();
                    tNode.Tag = m_ListPoints[i];
                    this.treeView1.Nodes.Add(tNode);

                }
            }
            if (m_Fields != null)
            {
                for (int i = 0; i < m_Fields.FieldCount; i++)
                {
                    IField tField = m_Fields.get_Field(i);
                    cmbX.Items.Add(tField.Name);
                    cmbY.Items.Add(tField.Name);
                    cmbZ.Items.Add(tField.Name);
                }
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            m_SelectNode = e.Node;
            TreeNode tNode = e.Node;// this.treeView1.SelectedNode;
            PointInfo tPointInfo = new PointInfo();
            //tNode.Tag=tPointInfo;
            if (tNode.Tag != null)
            {
                tPointInfo = tNode.Tag as PointInfo;
                if (tPointInfo != null)
                {
                    cmbX.Text = tPointInfo.x;
                    cmbY.Text = tPointInfo.y;
                    cmbZ.Text = tPointInfo.z;
                }
            }
            else
            {
                this.cmbX.Text = "";
                this.cmbY.Text = "";
                this.cmbZ.Text = "";
            }
        }

        private void tmsiNew_Click(object sender, EventArgs e)
        {
            TreeNode tNode = new TreeNode();
            tNode.Text = "点" +Convert.ToString( this.treeView1.Nodes.Count);

            this.treeView1.Nodes.Add(tNode);
            this.cmbX.Text = "";
            this.cmbY.Text = "";
            this.cmbZ.Text = "";

        }

        private void tmsiDelete_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Remove(this.treeView1.SelectedNode);

            RefreshTree();
        }

        private void tmsiCopy_Click(object sender, EventArgs e)
        {
            m_CopyNode = this.treeView1.SelectedNode;

        }

        private void tmsiPaste_Click(object sender, EventArgs e)
        {
            TreeNode tNode = m_CopyNode.Clone() as TreeNode;
            tNode.Text = "点" + Convert.ToString(this.treeView1.Nodes.Count);
            this.treeView1.Nodes.Insert(treeView1.SelectedNode.Index,tNode);
            RefreshTree();
        }

        private void RefreshTree()
        {
            for (int i = 0; i <treeView1.Nodes.Count; i++)
            {
                treeView1.Nodes[i].Text = "点" + i.ToString();
            }
            treeView1.Refresh();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbX_SelectedIndexChanged(object sender, EventArgs e)
        {
            PointInfo tPointInfo = new PointInfo();
            tPointInfo.x = cmbX.Text;
            tPointInfo.y = cmbY.Text;
            tPointInfo.z = cmbZ.Text;
            this.treeView1.Nodes[m_SelectNode.Index].Tag = tPointInfo;

            
        }

        private void cmbY_SelectedIndexChanged(object sender, EventArgs e)
        {
            PointInfo tPointInfo = new PointInfo();
            tPointInfo.x = cmbX.Text;
            tPointInfo.y = cmbY.Text;
            tPointInfo.z = cmbZ.Text;
            this.treeView1.Nodes[m_SelectNode.Index].Tag = tPointInfo;

        }

        private void cmbZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            PointInfo tPointInfo = new PointInfo();
            tPointInfo.x = cmbX.Text;
            tPointInfo.y = cmbY.Text;
            tPointInfo.z = cmbZ.Text;
            this.treeView1.Nodes[m_SelectNode.Index].Tag = tPointInfo;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            #region
            //switch (m_ConvertLayerSet.LayerType)
            //{
            //    case "点":
            //        if (treeView1.Nodes.Count != 1)
            //        {
            //            MessageBox.Show(m_ConvertLayerSet.PointItemName+"为点图层，需配置一个点坐标信息！");
            //            return;
            //        }
            //        break;
            //    case "线":

            //        if (treeView1.Nodes.Count < 2)
            //        {
            //            MessageBox.Show(m_ConvertLayerSet.LineItemName + "为线图层，需配置不少于两个点坐标信息！");
            //            return;
            //        }

            //        break;
            //    case "面":

            //        if (treeView1.Nodes.Count <3)
            //        {
            //            MessageBox.Show(m_ConvertLayerSet.Name + "为点面图层，需配置不少于三个点坐标信息！");
            //            return;
            //        }
            //        break;
            //    case "注记":

            //        break;
            //    case "属性表":
            //        break;

            //    default :
            //        break;
            //}
            #endregion

            m_ListPoints = new List<PointInfo>();
            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                PointInfo tPointInfo = (treeView1.Nodes[i].Tag) as  PointInfo;
                m_ListPoints.Add(tPointInfo);

            }
            this.Close();
        }
     
    }
}
