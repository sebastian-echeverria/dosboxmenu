// *********************************************************************************
// Title:		Helpers.cs
// Description:	Clase que contiene funciones importadas de Win32 para distintas cosas
// *********************************************************************************

using System;
using System.Runtime.InteropServices;
using System.Text;


namespace DosBoxMenu3
{
    public class Helpers
    {
        #region Win32 Imports
        [DllImport("kernel32.dll")]
        internal static extern Boolean SetCurrentDirectory([MarshalAs(UnmanagedType.LPTStr)]string lpPathName);

        [DllImport("kernel32.dll")]
        internal static extern uint GetFileAttributes([MarshalAs(UnmanagedType.LPTStr)]string lpPathName);
        internal const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;

        [DllImport("user32")]
        internal static extern int MessageBox(int hWnd, string text, string caption, int type);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern uint GetShortPathName(string lpszLongPath,
                                                    [Out] StringBuilder lpszShortPath,
                                                    uint cchBuffer);

        #endregion

        #region Wrappers

        // Retorna el path corto para DOS
        public static string ToShortPathName(string sLongName)
        {
            StringBuilder sShortNameBuffer = new StringBuilder(256);
            uint nBufferSize = (uint)sShortNameBuffer.Capacity;

            GetShortPathName(sLongName, sShortNameBuffer, nBufferSize);

            return sShortNameBuffer.ToString();
        }

        #endregion
    }
}
