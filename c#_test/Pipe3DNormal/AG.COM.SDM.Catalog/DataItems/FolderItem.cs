using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// �ļ��������� ��ժҪ˵����
    /// </summary>
    public class FolderItem : DataItem, IFileNameItem
    {
        private string m_Folder = "";

        /// <summary>
        /// ��ʼ���ļ���������ʵ������
        /// </summary>
        /// <param name="folder">�ļ�����</param>
        public FolderItem(string folder)
        {
            m_Folder = folder;
            m_DataType = DataType.dtFolder;
        }

        /// <summary>
        /// �����ļ�������
        /// </summary>
        public override string Name
        {
            get
            {
                if (m_Folder.EndsWith(@":\"))
                {
                    return m_Folder;
                }
                else
                {
                    return System.IO.Path.GetFileName(m_Folder);
                }
            }
        }

        /// <summary>
        /// �����ļ�������
        /// </summary>
        public string FileName
        {
            get { return m_Folder; }
        }

        private void AddChildrenToList(IWorkspace ws, IList<DataItem> items)
        {
            if (ws == null) return;

            DataItem item;
            IEnumDatasetName pEnum = ws.get_DatasetNames(esriDatasetType.esriDTAny);
            pEnum.Reset();
            IDatasetName ds = pEnum.Next();
            while (ds != null)
            {
                if (ds.Type == esriDatasetType.esriDTFeatureDataset)
                {
                    item = new FolderItem(ds.WorkspaceName.PathName);
                    items.Add(item);
                }
                ds = pEnum.Next();
            }
        }

        /// <summary>
        /// ��ȡ�ļ�
        /// </summary>
        /// <param name="Dir"></param>
        /// <returns>
        /// "GRID" - GRID
        /// "FOLDER" - ��ͨ�ļ���
        /// "TIN"	- tin
        /// GDB- file gdb
        ///   </returns>
        private string GetFolderType(string Dir)
        {
            string path = Dir;

            if (System.IO.Path.GetFileName(path).ToLower() == "info")
            {
                return "INFO";
            }

            if (path.ToLower().EndsWith(".gdb"))
                return "GDB";


            if (path[path.Length - 1] != '\\')
                path = path + "\\";
            //hdr.adf tdenv.adf

            if (System.IO.File.Exists(path + "hdr.adf"))
                return "GRID";
            if (System.IO.File.Exists(path + "tdenv.adf"))
                return "TIN";
            if (System.IO.File.Exists(path + "dbltic.adf"))
            {
                //�Ƿ���info�ļ���
                string infoPath = System.IO.Path.GetDirectoryName(Dir) + "\\info";
                if (System.IO.Directory.Exists(infoPath))
                {
                    if (System.IO.File.Exists(infoPath + "\\arc.dir"))
                        return "COVERAGE";
                }
            }
            // �ж��Ƿ�����Ƭ�ļ��У��ǵĻ�����դ�����ݼ�����
            if (System.IO.File.Exists(path + "conf.cdi") && System.IO.File.Exists(path + "conf.xml"))
                return "RasterDataset";

            return "FOLDER";
        }

        public override IList<DataItem> GetChildren()
        {
            IList<DataItem> items = new List<DataItem>();
            DataItem item;

            //�Ȳ��ҵ�ǰĿ¼������Щ��Ŀ¼�����жϸ���Ŀ¼�Ƿ�Ϊĳ���������Ŀ¼
            string[] Directories;
            try
            {
                Directories = System.IO.Directory.GetDirectories(m_Folder);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "����", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return null;
            }

            foreach (string Dir in Directories)
            {
                switch (GetFolderType(Dir))
                {
                    case "FOLDER":
                        item = new FolderItem(Dir);
                        item.Parent = this;
                        items.Add(item);
                        break;
                    case "TIN":
                        break;
                    case "GRID":
                        break;
                    case "GDB":
                        item = new FileGdbItem(Dir);
                        item.Parent = this;
                        items.Add(item);
                        break;
                    case "RasterDataset":
                        item = new ImageFileItem(Dir);
                        item.Parent = this;
                        items.Add(item);
                        break;
                }
            }

            string[] Filenames = System.IO.Directory.GetFiles(m_Folder);
            string ext;
            foreach (string filename in Filenames)
            {
                ext = System.IO.Path.GetExtension(filename).ToLower();
                if (ext == null) continue;

                item = CreateFileObject(filename, ext);
                if (item != null)
                {
                    item.Parent = this;
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// �����ļ�����
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="ext">��չ��</param>
        /// <returns>���ش������ļ�����</returns>
        private DataItem CreateFileObject(string fileName, string ext)
        {
            if (ext == null)
                return null;
            DataItem item = null;
            switch (ext.ToLower())
            {
                case ".bmp":
                    item = new ImageFileItem(fileName);
                    break;
                case ".jpg":
                    goto case ".bmp";
                case ".tif":
                    goto case ".bmp";
                case ".img":
                    goto case ".bmp";
                case ".sid":
                    goto case ".bmp";
                case ".mdb":
                    item = new AccessGdbItem(fileName);
                    break;
                case ".shp":
                    item = new ShapeFileItem(fileName);
                    break;
                case ".lyr":
                    item = new LayerFileItem(fileName);
                    break;
                case ".dwg":
                    item = new CadDrawingItem(fileName);
                    break;
                case ".dxf":
                    goto case ".dwg";
                case ".xls":
                case ".xlsx":
                    {
                        item = new ExcelFileItem(fileName);
                        break;
                    }
            }
            if (item != null)
                item.Parent = this;
            return item;
        }

        public override bool HasChildren
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// ��ȡ���Ƿ���Լ��ص���ͼ������������д򿪵�״ֵ̬
        /// </summary>
        public override bool CanLoad
        {
            get
            {
                return false;
            }
        }

        public override object GetGeoObject()
        {
            IWorkspaceFactory f = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactoryClass();
            IPropertySet pset = new PropertySetClass();
            pset.SetProperty("DATABASE", m_Folder);
            return f.Open(pset, 0);
        }

    }

    /// <summary>
    /// ���������� ժҪ˵��
    /// </summary>
    public class DiskItem : FolderItem
    {
        public DiskItem(string diskStr)
            : base(diskStr)
        {
            m_DataType = DataType.dtDisk;
        }

        public override object GetGeoObject()
        {
            IWorkspaceFactory f = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactoryClass();
            IPropertySet pset = new PropertySetClass();
            pset.SetProperty("DATABASE", base.FileName);
            return f.Open(pset, 0);
        }
    }
}
