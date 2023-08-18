using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 耗时技术
    /// </summary>
    public class ActionEx
    {
        public static long Execute(Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            action.Invoke();
            sw.Stop();
            Console.WriteLine($"<--耗时:{sw.ElapsedMilliseconds}\r\n");
            return sw.ElapsedMilliseconds;
        }
    }
}
