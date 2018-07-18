using System.Windows.Forms;

namespace SmartTaskbar
{
    class MsgFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case SafeNativeMethods.MSG_INSTANCE:

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
