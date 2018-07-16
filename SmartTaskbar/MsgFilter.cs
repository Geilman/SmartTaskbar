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
                    //MessageBox.Show("!");
                    return true;
                case SafeNativeMethods.MSG_MAX:
                    MessageBox.Show("What");
                    return true;
                case SafeNativeMethods.MSG_UNMAX:
                    MessageBox.Show("Now");
                    return true;
                default:
                    return false;
            }
        }
    }
}
