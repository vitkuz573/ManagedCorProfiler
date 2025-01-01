namespace Windows.Win32.Foundation;

[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "In this context it is clear enough that the 'S_' prefix is to indicate success error codes (in the domain of HRESULTs) and it is not to indicate a static member.")]
public readonly partial struct HRESULT : IEquatable<HRESULT>
{
    public bool Equals(int other) => this.Value == other;

    /// <summary>
    /// The source of the error code is .NET CLR.
    /// </summary>
    private const uint FACILITY_URT = 0x13; // 19

    private const uint SEVERITY_ERROR = 1;

    // These props should be optimized to live in the Non-GC Heap avoiding indirection while accessing them.
    // e.g:
    //
    // return HRESULT.E_FAIL
    // 
    // mov      eax, 0xFFFFFFFF80004005
    // ret
    //
    public static HRESULT MakeHRESULT(uint severity, uint facility, uint errorNo)
    {
        uint result = severity << 31;

        result |= facility << 16;
        result |= errorNo;

        return new HRESULT(unchecked((int)result));
    }

    public static HRESULT MakeClrErrorHRESULT(uint errorNo)
    {
        return MakeHRESULT(SEVERITY_ERROR, FACILITY_URT, errorNo);
    }
}
