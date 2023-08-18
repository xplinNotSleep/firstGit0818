using System.Drawing;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.GeoDataBase.DataBackup
{
    /// <summary>
    /// 数据备份插件类
    /// </summary>
    public class DataBackupCommand : BaseCommand, IUseIcon
    {
        IHookHelperEx m_HookHelperEx = null;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DataBackupCommand()
        {
            base.m_caption = "数据备份";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantGeoDataBase.STR_IMAGEPATH + "C21+.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantGeoDataBase.STR_IMAGEPATH + "C21_32.ico"));         
        }

        private Icon m_Icon32 = null;
        private Icon m_Icon = null;
        /// <summary>
        /// ico图标对象16*16
        /// </summary>
        public Icon Icon16
        {
            get { return m_Icon; }
        }
        /// <summary>
        /// ico图标对象32*32
        /// </summary>
        public Icon Icon32
        {
            get { return m_Icon32; }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            FormDataBackup tFormDataBackup = new FormDataBackup();
            tFormDataBackup.ShowDialog();
        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            m_HookHelperEx = new HookHelperEx();
            m_HookHelperEx.Hook = hook;
        }
    }
}
