using System.Runtime.InteropServices;
using Windows.Win32.System.Com;
using Windows.Win32.Foundation;

namespace ClrProfiling.ComInterop.Wrappers;

/// <summary>
/// managed --> native
/// </summary>
[DynamicInterfaceCastableImplementation]
internal unsafe interface IClassFactoryNativeWrapper : IClassFactory.Interface
{
    public static new unsafe HRESULT CreateInstance(IUnknown* pUnkOuter, Guid* riid, void** ppvObject)
    {
        throw new NotImplementedException();
    }

    public static new HRESULT LockServer(BOOL fLock)
    {
        throw new NotImplementedException();
    }
}
