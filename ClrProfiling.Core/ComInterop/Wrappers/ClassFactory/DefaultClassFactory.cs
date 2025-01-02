using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace ClrProfiling.ComInterop.Wrappers;

public class DefaultClassFactory(object profilerInstance) : IClassFactory.Interface
{
    public unsafe HRESULT CreateInstance([Optional] IUnknown* pUnkOuter, Guid* riid, void** ppvObject)
    {
        if (pUnkOuter != null)
        {
            *ppvObject = null;

            return new HRESULT(-2147221232);
        }

        if (riid == null || ppvObject == null)
        {
            return HRESULT.E_POINTER;
        }

        var guid = *riid;

        var cw = new CorProfilerComWrappers();

        nint ccwUnknown = cw.GetOrCreateComInterfaceForObject(profilerInstance, CreateComInterfaceFlags.None);

        var hr = Marshal.QueryInterface(ccwUnknown, in guid, out var ptr);

        if (hr != HRESULT.S_OK)
        {
            return new HRESULT(hr);
        }

        *ppvObject = (void*)ptr;

        return HRESULT.S_OK;
    }

    public HRESULT LockServer(BOOL fLock)
    {
        return HRESULT.S_OK;
    }
}
