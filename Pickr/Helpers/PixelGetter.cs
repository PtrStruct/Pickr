using Pickr.Misc;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace Pickr.Helpers
{
    internal class PixelGetter
    {

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);

        public Color GetCursorPosAndColor()
        {
            GetCursorPos(out POINT p);
            var hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, p.x, p.y);
            var color = Color.FromArgb((int)(pixel & 0xFF), (int)(pixel & 0xFF00) >> 8, (int)(pixel & 0xFF0000) >> 16);

            /* Release the device context reference */
            ReleaseDC(IntPtr.Zero, hdc);


            Debug.WriteLine($"X: {p.x}  - Y: {p.y}");
            Debug.WriteLine($"X: {p.x}  - Y: {p.y}");
            Debug.WriteLine($"Color: R: {color.R} G: {color.G} B: {color.B} - A: {color.A}");

            return color;
        }
    }
}