using Windows.Win32.System.Diagnostics.Debug;

namespace Windows.Win32.Foundation;

public readonly partial struct HRESULT
{
    private const uint SEVERITY_ERROR = 1;

    public static HRESULT MakeHRESULT(uint severity, FACILITY_CODE facility, uint errorNo)
    {
        var result = severity << 31;

        result |= (uint)facility << 16;
        result |= errorNo;

        return new HRESULT(unchecked((int)result));
    }

    public static HRESULT MakeClrErrorHRESULT(uint errorNo)
    {
        return MakeHRESULT(SEVERITY_ERROR, FACILITY_CODE.FACILITY_URT, errorNo);
    }
}
