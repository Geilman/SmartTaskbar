using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SmartTaskbar
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //Use a mutex to ensure single instance
            using (Mutex mutex = new Mutex(true, "{959d3545-aa5c-42a8-a327-6e2c079daa94}", out bool createNew))
            {
                if (createNew)
                {
                    Application.AddMessageFilter(new MsgFilter());
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    new SystemTray();
                    Application.Run();
                }
                else
                {
                    Process cp = Process.GetCurrentProcess();
                    Process ep = (from p in Process.GetProcessesByName(cp.ProcessName) where cp.Id != p.Id select p).FirstOrDefault();
                    if (ep != null)
                        SafeNativeMethods.PostThreadMessageW(ep.Threads[0].Id, SafeNativeMethods.MSG_INSTANCE, 0, 0);
                }
            }
        }
    }
}
