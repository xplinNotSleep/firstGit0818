using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;
using System.Drawing;

namespace AG.COM.SDM.GeoDataBase.DataBackup
{
    public class DataInportCommand : BaseCommand, IUseIcon
    {
        private IHookHelperEx m_hookHelperEx;

        public DataInportCommand()
        {
            m_hookHelperEx = new HookHelperEx();

            base.m_name = GetType().FullName;
            base.m_caption = "数据入库";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantGeoDataBase.STR_IMAGEPATH + "C10.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantGeoDataBase.STR_IMAGEPATH + "C10_32.ico"));          
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

        public override void OnCreate(object hook)
        {
            m_hookHelperEx.Hook = hook;
        }

        public override void OnClick()
        {
            FormDataInport tFormDataInprot = new FormDataInport();
            tFormDataInprot.ShowInTaskbar = false;
            tFormDataInprot.Show(m_hookHelperEx.Win32Window);
        }
    }
}
