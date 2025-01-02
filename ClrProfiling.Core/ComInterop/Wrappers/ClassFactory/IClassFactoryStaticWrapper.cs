using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace ClrProfiling.ComInterop.Wrappers;

internal class IClassFactoryStaticWrapper : IClassFactory.Interface
{
    private bool _isDisposed = false;

    public nint IClassFactoryInst { get; init; }

    private IClassFactoryStaticWrapper() { }

    public static IClassFactoryStaticWrapper? CreateIfSupported(nint ptr)
    {
        int hr = Marshal.QueryInterface(ptr, in IClassFactory.IID_Guid, out nint instance);

        if (hr != HRESULT.S_OK)
        {
            return default;
        }

        Console.WriteLine("CreateIfSupported");

        return new IClassFactoryStaticWrapper()
        {
            IClassFactoryInst = instance,
        };
    }

    ~IClassFactoryStaticWrapper()
    {
        DisposeInternal();
    }

    public void Dispose()
    {
        DisposeInternal();

        GC.SuppressFinalize(this);
    }

    void DisposeInternal()
    {
        if (_isDisposed)
        {
            return;
        }

        // [WARNING] This is unsafe for COM objects that have specific thread affinity.
        Marshal.Release(IClassFactoryInst);

        _isDisposed = true;
    }

    public unsafe HRESULT CreateInstance([Optional] IUnknown* pUnkOuter, Guid* riid, void** ppvObject)
    {
        Console.WriteLine("CreateInstance");

        return IClassFactoryNativeWrapper.CreateInstance(pUnkOuter, riid, ppvObject);
    }

    public HRESULT LockServer(BOOL fLock)
    {
        Console.WriteLine("LockServer");

        return IClassFactoryNativeWrapper.LockServer(fLock);
    }
}
