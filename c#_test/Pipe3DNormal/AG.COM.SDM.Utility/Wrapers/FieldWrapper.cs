using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.Utility.Wrapers
{
    /// <summary>
    /// ×Ö¶Î°ü×°Àà
    /// </summary>
    public class FieldWrapper
    {
        private IField m_Field = null;
       
        public FieldWrapper(IField field)
        {
            m_Field = field;
        }

        public IField Field
        {
            get { return m_Field; }
        }

        public override string ToString()
        {
            string str = m_Field.AliasName;
            if (str != null)
            {
                if (str.Length > 0)
                    return str;
                else
                    return m_Field.Name;
            }
            else
                return m_Field.Name;
        }

    }
}
