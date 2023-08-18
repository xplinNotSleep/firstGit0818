using System;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 建立历史存档 插件类 
    /// 注意:传递的hook对象类型应为IDataset
    /// </summary>
    internal sealed class EnableArchive : BaseCommand
    {
        private IDataset m_Dataset;

        /// <summary>
        /// 实例化该对象
        /// </summary>
        public EnableArchive()
        {
            this.m_caption = "建立历史存档";
            this.m_toolTip = "建立历史存档";
            this.m_name = "EnableArchive";
        }

        /// <summary>
        /// 获取当前插件的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (this.m_Dataset == null) return false;

                //查询接口引用
                IVersionedObject3 tVersionObject = this.m_Dataset as IVersionedObject3;
                if (tVersionObject != null)
                {
                    //获取版本注册信息
                    bool isRegistered,isMovingEditsToBase;                    
                    tVersionObject.GetVersionRegistrationInfo(out isRegistered,out isMovingEditsToBase);

                    if (isRegistered == true && isMovingEditsToBase == false)
                    {
                        IArchivableObject tArchiveObject = tVersionObject as IArchivableObject;
                        if (tArchiveObject.IsArchiving == false) return true;
                    }
                }
    
                return false;                
            }
        }

        /// <summary>
        /// 单击事件处理
        /// </summary>
        public override void OnClick()
        {
            IArchivableObject tArchivableObject = this.m_Dataset as IArchivableObject;
            tArchivableObject.EnableArchiving(null, null, true);
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
                    throw new Exception("传递的hook对象与IDataset类型不一致~.\r\n Source:At EnableArchive OnCreate(object hook)");
                }
            }
        }
    }
}
