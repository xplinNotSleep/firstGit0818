using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace AG.COM.SDM.Catalog
{
    /// <summary>
    /// 历史位置实例（单件类）
    /// </summary>
    public class HistoryKeeperInstance
    {
        private static HistoryKeeper m_Keeper;
        public static HistoryKeeper Instance
        {
            get
            {
                if (m_Keeper == null)
                    m_Keeper = new HistoryKeeper();
                return m_Keeper;
            }
        }
    }

    /// <summary>
    /// 历史位置
    /// </summary>
    public class HistoryKeeper
    {
        public HistoryKeeper()
        {
            //载入历史位置
            LoadHistories();
        }

        private StringCollection m_Histories = new StringCollection();

        /// <summary>
        /// 历史位置文本信息
        /// </summary>
        public StringCollection Histories
        {
            get { return m_Histories; }
        }

        private const int KeeperSize = 10;

        /// <summary>
        /// 载入历史位置
        /// </summary>
        private void LoadHistories()
        {
            m_Histories.Clear();
            string fn = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\hisloc.dat";
            if (System.IO.File.Exists(fn) == false) return;

            System.IO.StreamReader reader = new System.IO.StreamReader(fn);
            string ln = reader.ReadLine();
            int count = 0;
            while (ln != null)
            {
                if (ln.Trim().Length > 0)
                    m_Histories.Add(ln.ToLower());

                count++;
                if (count > KeeperSize) break;
                ln = reader.ReadLine();
            }
            reader.Close();
        }

        /// <summary>
        /// 保存历史位置
        /// </summary>
        public void SaveHistories()
        {
            string fn = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\hisloc.dat";
            System.IO.StreamWriter writer = new System.IO.StreamWriter(fn, false);
            int startIndex = 0;
            if (m_Histories.Count > KeeperSize)
                startIndex = m_Histories.Count - KeeperSize;
            for (int i = startIndex; i <= m_Histories.Count - 1; i++)
            {
                writer.WriteLine(m_Histories[i]);
            }
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// 添加历史位置
        /// </summary>
        /// <param name="his">历史位置文本</param>
        public void AddHistory(string his)
        {
            string s = his.ToLower().Trim();
            if (s.Length == 0) return;
            if (m_Histories.IndexOf(s) >= 0) return;
            m_Histories.Add(s);
        }

        /// <summary>
        /// 移除历史位置
        /// </summary>
        /// <param name="his">历史位置文本</param>
        public void RemoveHistory(string his)
        {
            m_Histories.Remove(his);
        }
    }
}