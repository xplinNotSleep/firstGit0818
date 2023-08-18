using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    public partial class ErrorForm : Form
    {
        private List<TreeNode> treeNodes;
        public ErrorForm(List<TreeNode> treeNodes)
        {
            InitializeComponent();
            this.treeNodes = treeNodes;
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {
            if (treeNodes != null)
            {
                foreach (TreeNode treeNode in treeNodes)
                {
                    treeView1.Nodes.Add(treeNode);
                }
            }
            treeView1.ExpandAll();
        }

        public void RefreshTreeview(List<TreeNode> treeNodes)
        {
            treeView1.Nodes.Clear();
            if (treeNodes != null)
            {
                foreach (TreeNode treeNode in treeNodes)
                {
                    treeView1.Nodes.Add(treeNode);
                }
            }
            treeView1.ExpandAll();
        }
    }
}
