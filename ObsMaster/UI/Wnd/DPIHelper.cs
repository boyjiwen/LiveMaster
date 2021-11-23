using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Reflection;
using System.Net;

namespace HuyaFX.UI.Wnd
{
    public class DPIHelper
    {
        private static double dpiVal = 0;
        public static double GetDpiScale()
        {
            try
            {
                if (dpiVal != 0)
                    return dpiVal;

                var graphics = Graphics.FromHwnd(IntPtr.Zero);

                dpiVal = graphics.DpiX / 96;

                return dpiVal;
            }
            catch { }
            return 1;
        }

        public static int GetCurrentDPI()
        {
            return (int)typeof(SystemParameters).GetProperty("Dpi", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null, null) ;
        }

        public static double GetCurrentDPIScaleFactor()
        {
            try
            {
                if (dpiVal != 0)
                    return dpiVal;

                dpiVal = (double)GetCurrentDPI() / 96;
                return dpiVal;
            }
            catch { }
            return 1;
        }


    }
}