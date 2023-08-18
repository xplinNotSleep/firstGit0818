using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Display;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.Utility.Controls
{
    public partial class StyleButton : Control
    {
        private ISymbol m_Symbol = null;
        public StyleButton()
        {
            InitializeComponent();
            this.Text = "";
        }

        public ISymbol Symbol
        {
            get
            {
                return m_Symbol;
            }
            set 
            {               
                m_Symbol = value; 
            }
        }

        private void StyleButton_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = e.ClipRectangle;
            r.Width = r.Width - 1;
            r.Height = r.Height - 1;
            e.Graphics.DrawRectangle(Pens.Black, r);
            if (m_Symbol == null)
                return;

            DisplayHelper.DrawSymbol(e.Graphics, this.Bounds, m_Symbol);
        }
    }
}

