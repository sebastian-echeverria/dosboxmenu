using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
  class Program
  {
    public static String GetShortPathName(String longPath)
    {
      StringBuilder shortPath = new StringBuilder(longPath.Length + 1);

      int returnValue = Program.GetShortPathName(longPath, shortPath, shortPath.Capacity);
      if (0 == returnValue)
      {
        PrintLastError();
        return longPath;
      }
      else
      {
        Console.WriteLine(returnValue);
      }

      return shortPath.ToString();
    }

    public static void PrintLastError()
    {
      int errorCode = GetLastError();
      Console.WriteLine("Error: <" + errorCode + ">");
    }

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern Int32 GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern Int32 GetLastError();

    static void Main(string[] args)
    {
      //Console.OutputEncoding = System.Text.Encoding.ASCII;
      string path = Program.GetShortPathName("D:\\Juegos\\Juegos\\Apogee\\Duke Nukem\\Duke Nukem\\Duke Nukem Full Paid");
      //System.Diagnostics.Debug.Write(path);
      Console.WriteLine("Path: <" + path + ">");

    }

  }
}
