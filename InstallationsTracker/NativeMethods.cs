using System;
using System.Runtime.InteropServices;
using System.Text;

namespace InstallationsTracker
{
    internal static class NativeMethods
    {
        // WINAPI PInvoke : force UNICODE WINAPI functions

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern uint RegOpenKeyEx(
            UIntPtr hKey,
            string lpSubKey,
            uint ulOptions,
            int samDesired,
            out IntPtr phkResult);

        [DllImport("advapi32.dll")]
        internal static extern uint RegCloseKey(
            IntPtr hKey);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern uint RegQueryValueEx(
            IntPtr hKey,
            string lpValueName,
            IntPtr lpReserved,
            ref uint lpType,
            StringBuilder lpData,
            ref uint lpcbData);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern uint RegQueryInfoKey(
            IntPtr hKey,
            [Out()]StringBuilder lpClass,
            ref uint lpcchClass,
            IntPtr lpReserved,
            out uint lpcSubKeys,
            out uint lpcbMaxSubKeyLen,
            out uint lpcbMaxClassLen,
            out uint lpcValues,
            out uint lpcbMaxValueNameLen,
            out uint lpcbMaxValueLen,
            IntPtr lpcbSecurityDescriptor,
            IntPtr lpftLastWriteTime);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern uint RegEnumKeyEx(
            IntPtr hKey,
            uint dwIndex,
            StringBuilder lpName,
            ref uint lpcchName,
            IntPtr lpReserved,
            IntPtr lpClass,
            IntPtr lpcchClass,
            IntPtr lpftLastWriteTime);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern uint RegEnumValue(
            IntPtr hKey,
            uint dwIndex,
            StringBuilder lpValueName,
            ref uint lpcchValueName,
            IntPtr lpReserved,
            IntPtr lpType,
            IntPtr lpData,
            IntPtr lpcbData);

        // kernel32.dll

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool QueryFullProcessImageName(
            [In]
            IntPtr hProcess,
            [In]
            int dwFlags,
            [Out]
            StringBuilder lpExeName,
            ref uint lpdwSize);

        // shell32.dll

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr CommandLineToArgvW(
            [MarshalAs(UnmanagedType.LPWStr)]
            string lpCmdLine,
            out int pNumArgs);

       
    }
}
