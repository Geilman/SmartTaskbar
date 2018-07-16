using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTaskbar
{
    public class Notifier
    {
        private readonly Process process = new Process();
        public Notifier()
        {
            process.StartInfo.FileName = Path.Combine(Environment.CurrentDirectory, Environment.Is64BitProcess ? "x64" : "x86", "TaskbarNotifier");
            process.StartInfo.Arguments = Process.GetCurrentProcess().Threads[0].Id.ToString();
            process.Start();
        }
    }
}
