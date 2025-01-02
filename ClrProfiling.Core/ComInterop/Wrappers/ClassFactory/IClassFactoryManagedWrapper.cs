using System.Runtime.InteropServices;
using Windows.Win32.System.Com;
using static System.Runtime.InteropServices.ComWrappers;

namespace ClrProfiling.ComInterop.Wrappers;

/// <summary>
/// native --> managed
/// </summary>
public static unsafe class IClassFactoryManagedWrapper
{
    [UnmanagedCallersOnly]
    public static unsafe int CreateInstance(nint _this, IUnknown* pUnkOuter, Guid* riid, void** ppvObject)
    {
        try
        {
            var hr = ComInterfaceDispatch
                .GetInstance<IClassFactory.Interface>((ComInterfaceDispatch*)_this)
                .CreateInstance(pUnkOuter, riid, ppvObject);

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
                .GetInstance<IClassFactory.Interface>((ComInterfaceDispatch*)_this)
                .LockServer(@lock);
        }
        catch (Exception ex)
        {
            return ex.HResult;
        }
    }
}
