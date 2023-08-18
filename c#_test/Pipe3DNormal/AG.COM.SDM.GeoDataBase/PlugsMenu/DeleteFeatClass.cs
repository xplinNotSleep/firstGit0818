using System;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 删除图层插件类
    /// 注意:传递的hook对象为IDataset类型
    /// </summary>
    internal sealed class DeleteFeatClass : BaseCommand
    {
        private IDataset m_Dataset;

        /// <summary>
        /// 实例化该对象
        /// </summary>
        public DeleteFeatClass()
        {
            this.m_caption = "删除图层";
            this.m_toolTip = "删除图层";
            this.m_name = "DeleteFeatClass";
        }

        /// <summary>
        /// 获取当前插件的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return (this.m_Dataset == null) ? false : true;
            }
        }

        /// <summary>
        /// OnClick处理方法
        /// </summary>
        public override void OnClick()
        {
            if (MessageBox.Show("该操作不可恢复,您是否确实需要删除此对象?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IDataset tDataset = this.m_Dataset as IDataset;
                tDataset.Delete();
            }
        }

        /// <summary>
        /// 创建时初始化
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            if (hook == null) return;

            if (hook is IDataset)
            {
                this.m_Dataset = hook as IDataset;
            }
            else
            {
                throw new Exception("传递的hook对象与IDataset类型不一致~.\r\n Source:At DeleteFeatClass OnCreate(object hook)");
            }
        }
    }
}
