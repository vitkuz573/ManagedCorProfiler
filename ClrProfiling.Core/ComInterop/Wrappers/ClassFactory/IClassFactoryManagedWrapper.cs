using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.ComWrappers;

namespace ClrProfiling.ComInterop.Wrappers
{
    /// <summary>
    /// native --> managed
    /// </summary>
    public static unsafe class IClassFactoryManagedWrapper
    {
        [UnmanagedCallersOnly]
        public static unsafe int CreateInstance(nint _this, nint outer, Guid* guid, nint* instance)
        {
            try
            {
                var hr = ComInterfaceDispatch
                    .GetInstance<IClassFactory>((ComInterfaceDispatch*)_this)
                    .CreateInstance(outer, guid, instance);

                return hr;
            }
            catch (Exception ex)
            {
                return ex.HResult;
            }
        }

        [UnmanagedCallersOnly]
        public static int LockServer(nint _this, bool @lock)
        {
            try
            {
                return ComInterfaceDispatch
                    .GetInstance<IClassFactory>((ComInterfaceDispatch*)_this)
                    .LockServers(@lock);
            }
            catch (Exception ex)
            {
                return ex.HResult;
            }
        }
    }
}
