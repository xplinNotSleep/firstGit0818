
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
    public partial class SkinForm : XtraForm
    {
        public static DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
        public SkinForm()
        {
            InitializeComponent();
            //defaultLookAndFeel.LookAndFeel.SetSkinStyle(CommonVariables.CurrentSkinName);
        }
    }
}
