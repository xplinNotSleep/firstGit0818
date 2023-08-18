using System;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 删除历史存档 插件类 
    /// 注意:传递的hook对象类型应为IDataset
    /// </summary>
    internal sealed class DisableArchive : BaseCommand
    {
        private IDataset m_Dataset;

        /// <summary>
        /// 实例化该对象
        /// </summary>
        public DisableArchive()
        {
            this.m_caption = "删除历史存档";
            this.m_toolTip = "删除历史存档";
            this.m_name = "DisableArchive";
        }

        /// <summary>
        /// 获取当前插件的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (this.m_Dataset != null)
                {
                    IArchivableObject tArchivableObj = this.m_Dataset as IArchivableObject;
                    if (tArchivableObj != null && tArchivableObj.IsArchiving == true) return true; 
                }

                return false;
            }
        }

        /// <summary>
        /// 单击事件处理
        /// </summary>
        public override void OnClick()
        {
            DialogResult tDlgResult = MessageBox.Show("是否删除与此对象相关联的所有历史存档?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
            
            //查询接口引用 
            IArchivableObject tArchiveObj = this.m_Dataset as IArchivableObject;

            if (tDlgResult == DialogResult.Yes)
            {    
                //删除所有相关的历史存档
                tArchiveObj.DisableArchiving(true, true);
            }
            else if (tDlgResult == DialogResult.No)
            {
                //仅删除其本身
                tArchiveObj.DisableArchiving(true, false);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 创建时初始化
        /// </summary>
        /// <param name="hook">hook对象，传递的hook对象类型应为IDataset</param>
        public override void OnCreate(object hook)
        {
            if (hook == null) return;

            if (hook is IDataset)
            {
                this.m_Dataset = hook as IDataset;
            }
            else
            {
                throw new Exception("传递的hook对象与IDataset类型不一致~.\r\n Source:At DisableArchive OnCreate(object hook)");
            }
        }
    }
}
