using System;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 注册版本插件类 
    /// 注意:传递的hook对象类型应为IDataset
    /// </summary>
    internal sealed class RegisterVersion : BaseCommand
    {
        private IDataset m_Dataset;

        /// <summary>
        /// 实例化该对象
        /// </summary>
        public RegisterVersion()
        {
            this.m_caption = "注册版本";
            this.m_toolTip = "注册版本";
            this.m_name = "RegisterVersion";
        }

        /// <summary>
        /// 获取当前插件的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (this.m_Dataset == null)
                    return false;
                else 
                {
                    IVersionedObject tVersionedObj = this.m_Dataset as IVersionedObject;
                    if (tVersionedObj == null) return false;
                    if (tVersionedObj.IsRegisteredAsVersioned == true) return false;
                    return true;
                }
            }
        }

        public override void OnClick()
        {
            //查询接口引用
            IVersionedObject3 tVersionedObject = this.m_Dataset as IVersionedObject3;

            //获取对话框返回结果
            DialogResult tDlgResult = MessageBox.Show("编辑该版本时,是否将编辑对象更新到上级版本?", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
            if (tDlgResult == DialogResult.Yes)
            {
                tVersionedObject.RegisterAsVersioned3(true);
            }
            else if (tDlgResult == DialogResult.No)
            {
                tVersionedObject.RegisterAsVersioned3(false);
            }
            else
            {
                //什么都不做
            }
        }

        /// <summary>
        /// 创建时初始化
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            if (hook != null)
            {
                if (hook is IDataset)
                {
                    this.m_Dataset = hook as IDataset;
                }
                else
                {
                    throw new Exception("传递的hook对象与IDataset类型不一致~.\r\n Source:At RegisterVersion OnCreate(object hook)");
                }
            }
        }
    }
}
