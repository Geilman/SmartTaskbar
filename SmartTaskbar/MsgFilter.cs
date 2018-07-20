using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SmartTaskbar
{
    class MsgFilter : IMessageFilter
    {
        private readonly AutoMode autoMode = new AutoMode();

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case SafeNativeMethods.MSG_INSTANCE:
                    Process.Start(Path.Combine(Environment.CurrentDirectory, "SmartTaskbar.Settings"));
                    return true;
                case SafeNativeMethods.MSG_MAX:
                    
                    return true;
                case SafeNativeMethods.MSG_UNMAX:
                    
                    return true;
                default:
                    return false;
            }
        }
    }
}
