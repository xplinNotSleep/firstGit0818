using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Display;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.Utility.Controls
{
    public partial class NorthArrowControl : Control
    {
        public NorthArrowControl()
        {
            InitializeComponent();
            this.BackColor = Color.White;
        }

        private bool m_IsVisible = true;
        public bool IsVisible
        {
            get { return m_IsVisible; }
            set { m_IsVisible = value; }
        }

        private ISymbol m_Symbol = null;
        public ISymbol Symbol
        {
            get
            {
                return m_Symbol;
            }
            set { m_Symbol = value; }
        }

        //重绘自定义控件
        private void NorthArrowControl_Paint(object sender, PaintEventArgs e)
        {
            if (m_Symbol == null)
                return;

            if (m_IsVisible == false)
                return;

            if (m_Symbol is IMarkerSymbol)
                (m_Symbol as IMarkerSymbol).Size = 48;
            DisplayHelper.DrawSymbol(e.Graphics, this.Bounds, m_Symbol);
        }
    }
}
