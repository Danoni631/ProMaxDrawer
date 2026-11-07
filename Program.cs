using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Runtime.InteropServices;

namespace ProMaxDrawer
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public static class Stuff
    {
        public enum Rops
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            SRCERASE = 0x00440328,
            NOTSRCINVERT = 0x999999,
            MERGECOPY = 0x00C000CA,
            PATCOPY = 0x00F00021,
            PATINVERT = 0x005A0049,
            SRCRNBW = 0x00E20746,
        };
    }

    public static class Drawers
    {

        [DllImport("gdi32.dll")]
        static extern bool PatBlt(IntPtr hdc, int nXLeft, int nYLeft, int nWidth, int nHeight, uint dwRop);

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt
        (
            IntPtr hdcDest,
            int nXDest, int nYDest,
            int nWidth, int nHeight,
            IntPtr hdcSrc,
            int nXSrc, int nYSrc,
            uint dwRop
        );

        [DllImport("gdi32.dll")] static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int w, int h);

        [DllImport("gdi32.dll")] static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")] static extern IntPtr CreateHatchBrush(int iHatch, int color);
        [DllImport("gdi32.dll")] static extern int SetBkColor(IntPtr hdc, int crColor);

        static int w = GetSystemMetrics(0);
        static int h = GetSystemMetrics(1);

        public static void Texts()
        {
            Random random = new Random();

            while (true)
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                IntPtr dcCopy = CreateCompatibleDC(hdc);
                IntPtr hbit = CreateCompatibleBitmap(hdc, w, h);
                SelectObject(dcCopy, hbit);
                BitBlt(dcCopy, 0, 0, w, h, hdc, 0, 0, (uint)Stuff.Rops.SRCCOPY);
                Graphics graphics = Graphics.FromHdc(dcCopy);
                graphics.RotateTransform(360);

                Brush brush = new SolidBrush
                (
                    Color.FromArgb
                    (
                        random.Next(255),
                        random.Next(255),
                        random.Next(255)
                    )
                );

                graphics.DrawString
                (
                    "string",
                    new Font("Arial", random.Next(1, 102)),
                    brush,
                    random.Next(w),
                    random.Next(h)
                );

                BitBlt(hdc, 0, 0, w, h, dcCopy, 0, 0, (uint)Stuff.Rops.SRCCOPY);
                Thread.Sleep(1);
                DeleteObject(hbit);
                DeleteObject(dcCopy);
            }
        }

        public static void BarakoEffect()
        {
            Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
            Random random = new Random();

            while (true)
            {
                Pen barako = new Pen
                (
                    Color.FromArgb
                    (
                        random.Next(255),
                        random.Next(255),
                        random.Next(255),
                        random.Next(255)
                    ),

                    random.Next(70)
                );

                barako.LineJoin = LineJoin.Round;
                barako.StartCap = barako.EndCap = LineCap.Round;

                Point[] point =
                {
                    new Point
                    (
                        random.Next(-w, w + w),
                        random.Next(-h, h + h)
                    ),

                    new Point
                    (
                        random.Next(-w, w + w),
                        random.Next(-h, h + h)
                    )
                };

                graphics.DrawLines(barako, point);
            }
        }

        public static void DarkR()
        {
            Random random = new Random();

            while (true)
            {
                IntPtr hdc = GetDC(IntPtr.Zero);

                BitBlt
                (
                    hdc,
                    random.Next(2), random.Next(2),
                    w, h,
                    hdc,
                    random.Next(2), random.Next(2),
                    (uint)Stuff.Rops.SRCAND
                );

                ReleaseDC(IntPtr.Zero, hdc);
                Thread.Sleep(10);
            }
        }

        public static void BrightR()
        {
            Random random = new Random();

            while (true)
            {
                IntPtr hdc = GetDC(IntPtr.Zero);

                BitBlt
                (
                    hdc,
                    random.Next(2), random.Next(2),
                    w, h,
                    hdc,
                    random.Next(2), random.Next(2),
                    (uint)Stuff.Rops.SRCPAINT
                );

                ReleaseDC(IntPtr.Zero, hdc);
                Thread.Sleep(10);
            }
        }

        public static void Shake()
        {
            while (true)
            {
                IntPtr hdc = GetDC(IntPtr.Zero);

                BitBlt
                (
                    hdc,
                    5, 0,
                    w, h,
                    hdc,
                    0, 0,
                    (uint)Stuff.Rops.SRCCOPY
                );

                Thread.Sleep(1);

                BitBlt
                (
                    hdc,
                    5, 5,
                    w, h,
                    hdc,
                    0, 0,
                    (uint)Stuff.Rops.SRCCOPY
                );

                Thread.Sleep(1);

                BitBlt
                (
                    hdc,
                    0, 5,
                    w, h,
                    hdc,
                    0, 0,
                    (uint)Stuff.Rops.SRCCOPY
                );

                Thread.Sleep(1);

                BitBlt
                (
                    hdc,
                    0, 5,
                    w, h,
                    hdc,
                    5, 0,
                    (uint)Stuff.Rops.SRCCOPY
                );

                Thread.Sleep(1);

                BitBlt
                (
                    hdc,
                    0, 0,
                    w, h,
                    hdc,
                    5, 0,
                    (uint)Stuff.Rops.SRCCOPY
                );

                Thread.Sleep(1);

                BitBlt
                (
                    hdc,
                    0, 0,
                    w, h,
                    hdc,
                    5, 5,
                    (uint)Stuff.Rops.SRCCOPY
                );

                Thread.Sleep(1);

                BitBlt
                (
                    hdc,
                    0, 0,
                    w, h,
                    hdc,
                    0, 5,
                    (uint)Stuff.Rops.SRCCOPY
                );

                Thread.Sleep(1);

                BitBlt
                (
                    hdc,
                    5, 0,
                    w, h,
                    hdc,
                    0, 5,
                    (uint)Stuff.Rops.SRCCOPY
                );

                Thread.Sleep(1);

                ReleaseDC(IntPtr.Zero, hdc);
            }
        }

        public static void Invert()
        {
            Random random = new Random();

            while (true)
            {
                IntPtr hdc = GetDC(IntPtr.Zero);

                BitBlt
                (
                    hdc,
                    random.Next(2), random.Next(2),
                    w, h,
                    hdc,
                    random.Next(2), random.Next(2),
                    (uint)Stuff.Rops.SRCINVERT
                );

                ReleaseDC(IntPtr.Zero, hdc);
                Thread.Sleep(10);
            }
        }

        public static void Hatch()
        {
            Random random = new Random();
            uint[] rndclr = { 0x0000FF, 0xBC00FF, 0x33FF00, 0x00F7FF, 0xEF0000 };

            while (1)
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                IntPtr dcCopy = CreateCompatibleDC(hdc);

                IntPtr brush = CreateHatchBrush(random.Next(4), random.Next(255));

                while (true)
                {
                    uint color = rndclr[random.Next(rndclr.Length)];
                    SelectObject(hdc, brush);
                    SetBkColor(hdc, color));
                    PatBlt(hdc, 0, 0, w, h, PATINVERT);
                    Sleep(1);
                }

                DeleteObject(brush);
                DeleteDC(hdc);
                return 0;
            }
        }
    }
}
