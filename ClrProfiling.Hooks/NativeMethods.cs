using System.Runtime.InteropServices;

namespace ClrProfiling.Hooks
{
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern nint LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern nint GetProcAddress(nint hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(nint hModule);

        [DllImport("kernel32.dll")]
        public static extern nint GetModuleHandle(nint moduleName);
    }
}