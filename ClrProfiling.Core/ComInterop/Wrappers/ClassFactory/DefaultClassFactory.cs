using System.Diagnostics;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace ClrProfiling.ComInterop.Wrappers;

public class DefaultClassFactory : IClassFactory
{
    private readonly object _profiler;

    public DefaultClassFactory(object profilerInstance)
    {
        _profiler = profilerInstance;
    }

    public unsafe int CreateInstance(nint outer, Guid* guid, nint* instance)
    {
        if (outer != nint.Zero)
        {
            *instance = 0;

            return -2147221232; // E_NO_AGGREGATE
        }

        var guid_ = *guid;

        var cw = new CorProfilerComWrappers();

        nint ccwUnknown = cw.GetOrCreateComInterfaceForObject(
                _profiler,
                CreateComInterfaceFlags.None);

        var hr = Marshal.QueryInterface(ccwUnknown, in guid_, out var ptr);

        Debug.Assert(hr == 0);

        *instance = ptr;

        return HRESULT.S_OK;

        // var queryInterface = (delegate* unmanaged<nint, Guid*, void**, int>)&ICorProfilerCallback_.QueryInterface;
        //return queryInterface(nint.Zero, guid, (void**)instance);
    }

    public int LockServers(bool @lock)
    {
        return HRESULT.S_OK;
    }
}
