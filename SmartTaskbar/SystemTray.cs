using System.Windows.Forms;

namespace SmartTaskbar
{
    public class SystemTray
    {
        private readonly NotifyIcon notifyIcon;
        private readonly Notifier notifier = new Notifier();
        public SystemTray()
        {
            ToolStripMenuItem exit = new ToolStripMenuItem
            {
                Text = System.Threading.Thread.CurrentThread.CurrentUICulture.Name.StartsWith("zh") ? "退出" : "Exit",
                Font = new System.Drawing.Font("Segoe UI", 9F)
            };
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add(exit);

            notifyIcon = new NotifyIcon
            {
                Icon = System.Environment.OSVersion.Version.Major.ToString() == "10" ? Properties.Resources.logo_32 : Properties.Resources.logo_blue_32,
                ContextMenuStrip = contextMenuStrip,
                Text = "SmartTaskbar v1.2.0",
                Visible = true
            };

            exit.Click += (s, e) =>
            {
                notifier.Stop();
                notifyIcon.Dispose();
                Application.Exit();
            };

            notifyIcon.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    
                }
                notifier.Resume();
            };
        }
    }
}