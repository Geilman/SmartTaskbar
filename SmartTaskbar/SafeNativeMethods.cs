using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SmartTaskbar
{
    [SuppressUnmanagedCodeSecurity]
    class SafeNativeMethods
    {
        static SafeNativeMethods()
        {

        }

        #region SendMessage

        public const int MSG_INSTANCE = 0x500;
        public const int MSG_MAX = 0x501;
        public const int MSG_UNMAX = 0x502;

        /// Return Type: BOOL->int
        ///idThread: DWORD->unsigned int
        ///Msg: UINT->unsigned int
        ///wParam: WPARAM->UINT_PTR->unsigned int
        ///lParam: LPARAM->LONG_PTR->int
        [DllImport("user32.dll", EntryPoint = "PostThreadMessageW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostThreadMessageW(int idThread, int Msg, int wParam, int lParam);

        #endregion
    }
}
