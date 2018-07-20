using System;
using System.Diagnostics;
using System.IO;

namespace SmartTaskbar
{
    public class Notifier
    {
        private readonly Process process = new Process();
        /// <summary>
        /// Start process
        /// </summary>
        public Notifier()
        {
            process.StartInfo.FileName = Path.Combine(Environment.CurrentDirectory, Environment.Is64BitProcess ? "x64" : "x86", "TaskbarNotifier");
            process.StartInfo.Arguments = Process.GetCurrentProcess().Threads[0].Id.ToString();
            process.Start();
            SafeNativeMethods.AddProcess(process.Handle);
        }
        /// <summary>
        /// Restart process, if it is terminated unexpectedly
        /// </summary>
        public void Resume()
        {
            if (process.HasExited)
            {
                process.Start();
                SafeNativeMethods.AddProcess(process.Handle);
            }
        }
        /// <summary>
        /// Shutdown process
        /// </summary>
        public void Stop()
        {
            if (!process.HasExited)
            {
                process.Kill();
                process.WaitForExit();
            }
        }
    }
}
