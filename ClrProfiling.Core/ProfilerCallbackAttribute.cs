namespace ClrProfiling.Core;

[AttributeUsage(AttributeTargets.Class)]
public class ProfilerCallbackAttribute(string guid) : Attribute
{
    public Guid IID { get; private set; } = new Guid(guid);
}
