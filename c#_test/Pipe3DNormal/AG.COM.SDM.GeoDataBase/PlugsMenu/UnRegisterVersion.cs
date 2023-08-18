using System;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 取消注册插件类
    /// 注意:传递的hook对象应为IDataset类型
    /// </summary>
    internal sealed class UnRegisterVersion : BaseCommand
    {
        private IDataset m_Dataset;

        /// <summary>
        /// 实例化该对象
        /// </summary>
        public UnRegisterVersion()
        {
            this.m_caption = "取消注册";
            this.m_toolTip = "取消注册";
            this.m_name = "UnRegisterVersion";
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
                    //查询接口引用
                    IVersionedObject3 tVersionedObject = this.m_Dataset as IVersionedObject3;
                    if (tVersionedObject != null && tVersionedObject.IsRegisteredAsVersioned)
                    {
                        //查询接口引用
                        IArchivableObject tArchivableObject = tVersionedObject as IArchivableObject;
                        if (tArchivableObject.IsArchiving == false)
                        {
                            return true;
                        }      
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
            IVersionedObject3 tVersionedObject = this.m_Dataset as IVersionedObject3;
            tVersionedObject.UnRegisterAsVersioned3(false);
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
                throw new Exception("传递的hook对象与IDataset类型不一致~.\r\n Source:At UnRegisterVersion OnCreate(object hook)");
            }
        }
    }
}
