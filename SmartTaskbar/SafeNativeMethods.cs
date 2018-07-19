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
            #region Job

            int length = Marshal.SizeOf(typeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
            IntPtr extendedInfoPtr = Marshal.AllocHGlobal(length);
            try
            {
                Marshal.StructureToPtr(new JOBOBJECT_EXTENDED_LIMIT_INFORMATION
                {
                    BasicLimitInformation = new JOBOBJECT_BASIC_LIMIT_INFORMATION
                    {
                        LimitFlags = 0x2000
                    }
                }, extendedInfoPtr, false);
                SetInformationJobObject(s_jobHandle, 9, extendedInfoPtr, (uint)length);
            }
            finally
            {
                Marshal.FreeHGlobal(extendedInfoPtr);
            }

            #endregion
        }

        #region Job Container

        private static readonly IntPtr s_jobHandle = CreateJobObjectW(IntPtr.Zero, null);

        /// <summary>
        /// Add process to current Job
        /// </summary>
        /// <param name="handle">Process handle</param>
        public static void AddProcess(IntPtr handle)
        {
            if (s_jobHandle == IntPtr.Zero)
                return;
            AssignProcessToJobObject(s_jobHandle, handle);
        }


        /// Return Type: HANDLE->IntPtr
        ///lpJobAttributes: LPSECURITY_ATTRIBUTES->IntPtr
        ///lpName: LPCWSTR->string
        [DllImport("kernel32.dll", EntryPoint = "CreateJobObjectW")]
        private static extern IntPtr CreateJobObjectW(IntPtr lpJobAttributes, [MarshalAs(UnmanagedType.LPWStr)] string lpName);

        /// Return Type: BOOL->bool
        ///hJob: HANDLE->IntPtr
        ///JobObjectInformationClass: JOBOBJECTINFOCLASS->int
        ///lpJobObjectInformation: LPVOID->IntPtr
        ///cbJobObjectInformationLength: DWORD->uint
        [DllImport("kernel32.dll", EntryPoint = "SetInformationJobObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetInformationJobObject(IntPtr hJob, int JobObjectInformationClass, IntPtr lpJobObjectInformation, uint cbJobObjectInformationLength);


        /// Return Type: BOOL->bool
        ///hJob: HANDLE->IntPtr
        ///hProcess: HANDLE->IntPtr
        [DllImport("kernel32.dll", EntryPoint = "AssignProcessToJobObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AssignProcessToJobObject([In()] IntPtr hJob, [In()] IntPtr hProcess);

        [StructLayout(LayoutKind.Sequential)]
        private struct JOBOBJECT_BASIC_LIMIT_INFORMATION
        {
            public Int64 PerProcessUserTimeLimit;
            public Int64 PerJobUserTimeLimit;
            public UInt32 LimitFlags;
            public UIntPtr MinimumWorkingSetSize;
            public UIntPtr MaximumWorkingSetSize;
            public UInt32 ActiveProcessLimit;
            public Int64 Affinity;
            public UInt32 PriorityClass;
            public UInt32 SchedulingClass;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct IO_COUNTERS
        {
            public UInt64 ReadOperationCount;
            public UInt64 WriteOperationCount;
            public UInt64 OtherOperationCount;
            public UInt64 ReadTransferCount;
            public UInt64 WriteTransferCount;
            public UInt64 OtherTransferCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
        {
            public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
            public IO_COUNTERS IoInfo;
            public UIntPtr ProcessMemoryLimit;
            public UIntPtr JobMemoryLimit;
            public UIntPtr PeakProcessMemoryUsed;
            public UIntPtr PeakJobMemoryUsed;
        }

        #endregion


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
