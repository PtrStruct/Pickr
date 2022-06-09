using System.Runtime.InteropServices;

namespace Pickr.Misc
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }
}