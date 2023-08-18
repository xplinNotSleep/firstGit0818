using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.Utility
{
   public class ZipHelper
    {
        #region 制作压缩包（单个文件压缩）
        /// <summary>
        /// 制作压缩包（单个文件压缩）
        /// </summary>
        /// <param name="sourceFileName">原文件</param>
        /// <param name="zipFileName">压缩文件</param>
        /// <param name="zipEnum">压缩算法枚举</param>
        /// <returns>压缩成功标志</returns>
        public static bool ZipFile(string srcFileName, string zipFileName, ZipEnum zipEnum)
        {
            bool flag = true;
            try
            {
                switch (zipEnum)
                {
                    case ZipEnum.BZIP2:

                        FileStream inStream = File.OpenRead(srcFileName);
                        FileStream outStream = File.Open(zipFileName, FileMode.Create);

                        //参数true表示压缩完成后，inStream和outStream连接都释放
                        BZip2.Compress(inStream, outStream, true, 4096);

                        inStream.Close();
                        outStream.Close();


                        break;
                    case ZipEnum.GZIP:

                        FileStream srcFile = File.OpenRead(srcFileName);

                        GZipOutputStream zipFile = new GZipOutputStream(File.Open(zipFileName, FileMode.Create));

                        byte[] fileData = new byte[srcFile.Length];
                        srcFile.Read(fileData, 0, (int)srcFile.Length);
                        zipFile.Write(fileData, 0, fileData.Length);

                        srcFile.Close();
                        zipFile.Close();

                        break;
                    default: break;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
        #endregion

        #region 解压缩包（单个文件解压缩）
        /// <summary>
        /// 解压缩包（单个文件解压缩）
        /// </summary>
        /// <param name="zipFileName">压缩文件</param>
        /// <param name="unzipFileName">解压缩文件</param>
        /// <param name="zipEnum">压缩算法枚举</param>
        /// <returns>压缩成功标志</returns>
        public static bool UnZipFile(string zipFileName, string unzipFileName, ZipEnum zipEnum)
        {
            bool flag = true;
            try
            {
                switch (zipEnum)
                {
                    case ZipEnum.BZIP2:
                        FileStream inStream = File.OpenRead(zipFileName);
                        FileStream outStream = File.Open(unzipFileName, FileMode.Create);
                        BZip2.Decompress(inStream, outStream, true);
                        break;
                    case ZipEnum.GZIP:
                        GZipInputStream zipFile = new GZipInputStream(File.OpenRead(zipFileName));
                        FileStream destFile = File.Open(unzipFileName, FileMode.Create);

                        int bufferSize = 2048 * 2;
                        byte[] fileData = new byte[bufferSize];

                        while (bufferSize > 0)
                        {
                            bufferSize = zipFile.Read(fileData, 0, bufferSize);
                            zipFile.Write(fileData, 0, bufferSize);
                        }
                        destFile.Close();
                        zipFile.Close();
                        break;
                    default: break;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
        #endregion

        #region 制作压缩包（多个文件压缩到一个压缩包，支持加密、注释）
        /// <summary>
        /// 制作压缩包（多个文件压缩到一个压缩包，支持加密、注释）
        /// </summary>
        /// <param name="topDirectoryName">压缩文件目录</param>
        /// <param name="zipedFileName">压缩包文件名</param>
        /// <param name="compresssionLevel">压缩级别 1-9</param>
        /// <param name="password">密码</param>
        /// <param name="comment">注释</param>
        public static void ZipFiles(string topDirectoryName, string zipedFileName, int compresssionLevel, string password, string comment)
        {
            using (ZipOutputStream zos = new ZipOutputStream(File.Open(zipedFileName, FileMode.OpenOrCreate)))
            {
                if (compresssionLevel != 0)
                {
                    zos.SetLevel(compresssionLevel);//设置压缩级别
                }

                if (!string.IsNullOrEmpty(password))
                {
                    zos.Password = password;//设置zip包加密密码
                }

                if (!string.IsNullOrEmpty(comment))
                {
                    zos.SetComment(comment);//设置zip包的注释
                }

                //循环设置目录下所有的*.jpg文件（支持子目录搜索）
                foreach (string file in Directory.GetFiles(topDirectoryName, "*.*", SearchOption.AllDirectories))
                {
                    if (File.Exists(file))
                    {
                        FileInfo item = new FileInfo(file);
                        FileStream fs = File.OpenRead(item.FullName);
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);

                        ZipEntry entry = new ZipEntry(item.Name);
                        zos.PutNextEntry(entry);
                        zos.Write(buffer, 0, buffer.Length);
                    }
                }

                         
            }
        }
        #endregion
        #region 解压缩包（将压缩包解压到指定目录）
        /// <summary>
        /// 解压缩包（将压缩包解压到指定目录）
        /// </summary>
        /// <param name="zipedFileName">压缩包名称</param>
        /// <param name="unZipDirectory">解压缩目录</param>
        /// <param name="password">密码</param>
        public static void UnZipFiles(string zipedFileName, string unZipDirectory, string password)
        {
            using (ZipInputStream zis = new ZipInputStream(File.Open(zipedFileName, FileMode.OpenOrCreate)))
            {
                if (!string.IsNullOrEmpty(password))
                {
                    zis.Password = password;//有加密文件的，可以设置密码解压
                }

                ZipEntry zipEntry;
                while ((zipEntry = zis.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(unZipDirectory);
                    string pathName = Path.GetDirectoryName(zipEntry.Name);
                    string fileName = Path.GetFileName(zipEntry.Name);

                    pathName = pathName.Replace(".", "$");
                    directoryName += "\\" + pathName;

                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        FileStream fs = File.Create(Path.Combine(directoryName, fileName));
                        int size = 2048;
                        byte[] bytes = new byte[2048];
                        while (true)
                        {
                            size = zis.Read(bytes, 0, bytes.Length);
                            if (size > 0)
                            {
                                fs.Write(bytes, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        fs.Close();
                    }
                }
            }
        }
        #endregion
    }
    // <summary>
    /// 压缩枚举
    /// </summary>
    public enum ZipEnum
    {
        //压缩时间长，压缩率高
        BZIP2,

        //压缩效率高，压缩率低
        GZIP
    }
}
