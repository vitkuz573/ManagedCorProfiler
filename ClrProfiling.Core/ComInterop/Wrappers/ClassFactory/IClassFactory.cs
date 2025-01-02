namespace ClrProfiling.ComInterop.Wrappers;

public interface IClassFactory
{
    public static Guid IID_IClassFactory = new("00000001-0000-0000-C000-000000000046");

    public unsafe int CreateInstance(nint outer, Guid* guid, nint* instance);

    public int LockServers(bool @lock);
}
