using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// 文件夹数据项 的摘要说明。
    /// </summary>
    public class FolderItem : DataItem, IFileNameItem
    {
        private string m_Folder = "";

        /// <summary>
        /// 初始化文件夹数据项实例对象
        /// </summary>
        /// <param name="folder">文件名称</param>
        public FolderItem(string folder)
        {
            m_Folder = folder;
            m_DataType = DataType.dtFolder;
        }

        /// <summary>
        /// 返回文件夹名称
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
        /// 返回文件夹名称
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
        /// 获取文件
        /// </summary>
        /// <param name="Dir"></param>
        /// <returns>
        /// "GRID" - GRID
        /// "FOLDER" - 普通文件夹
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
                //是否有info文件夹
                string infoPath = System.IO.Path.GetDirectoryName(Dir) + "\\info";
                if (System.IO.Directory.Exists(infoPath))
                {
                    if (System.IO.File.Exists(infoPath + "\\arc.dir"))
                        return "COVERAGE";
                }
            }
            // 判断是否是切片文件夹，是的话当做栅格数据集返回
            if (System.IO.File.Exists(path + "conf.cdi") && System.IO.File.Exists(path + "conf.xml"))
                return "RasterDataset";

            return "FOLDER";
        }

        public override IList<DataItem> GetChildren()
        {
            IList<DataItem> items = new List<DataItem>();
            DataItem item;

            //先查找当前目录下有哪些子目录，并判断该子目录是否为某数据项的子目录
            string[] Directories;
            try
            {
                Directories = System.IO.Directory.GetDirectories(m_Folder);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
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
        /// 创建文件对象
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="ext">扩展名</param>
        /// <returns>返回创建的文件对象</returns>
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
        /// 获取其是否可以加载到地图或在软件界面中打开的状态值
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
    /// 磁盘数据项 摘要说明
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
